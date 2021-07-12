<template>
  <v-data-table :headers="headers" :items="employees" class="elevation-1">
    <template v-slot:top>
      <edit-employee
        :visible="showEditDialog"
        @close="showEditDialog = false"
        :editedEmployee="editedItem"
        @createdNewEmployee="getNewData"
      >
      </edit-employee>
      <v-toolbar flat>
        <v-spacer></v-spacer>
        <new-employee @createdNewEmployee="getNewData"></new-employee>

        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">
              Do you want to archive this employee?
            </v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="deleteItemNoArchive"
                >No</v-btn
              >
              <v-btn color="red darken-1" text @click="deleteItemArchive">
                Yes
              </v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>

    <template v-slot:[`item.actions`]="{ item }">
      <v-icon
        small
        class="mr-2"
        @click="editItem(item)"
        
      >
        mdi-pencil
      </v-icon>
      <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
    </template>
    <template v-slot:no-data>
      <v-btn color="primary" @click="retrieveEmployees"> Reset </v-btn>
    </template>
  </v-data-table>
</template>


<script>
import EmployeeService from "../Services/EmployeeService";
import WorkPositionService from "../Services/WorkPositionService";
import NewEmployee from "../components/NewEmployee.vue";
import EditEmployee from "../components/EditEmployee.vue";

export default {
  name: "Active",
  components: {
    NewEmployee,
    EditEmployee,
  },
  data: () => ({
    showEditDialog: false,
    date: null,
    menu: false,
    positions: [],
    menu2: false,
    dialog: false,
    dialogDelete: false,
    headers: [
      { text: "Name", align: "start", value: "fullName" },
      { text: "Position", value: "workPositionName" },
      { text: "Actions", value: "actions", sortable: false },
    ],
    employees: [],
  
    editedIndex: -1,
    editedItem: {
      name: "",
      surname: "",
      birthDate: "",
      workPositionName: "",
      salary: 0,
      address: "",
      id: 0,
      historyOfWorkPositions: [],
    },
    deletedItem: {
      id: 0,
      deletedDate: "",
    },
    defaultItem: {
      name: "",
      surname: "",
      position: "",
      lastPositions: "",
    },
  }),
  watch: {
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },
  created() {
    this.retrieveEmployees();
  },

  methods: {
    getNewData(value) {
      console.log(value);
      this.retrieveEmployees();
    },
    retrieveEmployees() {
      EmployeeService.getAllEmployees()
        .then((response) => {
          response = response.data;
          this.employees = [];
          response.forEach((element) => {
            this.employees.push({
              fullName: element.name + " " + element.surname,
              name: element.name,
              surname: element.surname,
              workPositionName: element.workPositionName,
              id: element.id,
              salary: element.salary,
              birthDate: element.birthDate,
              address: element.address,
              deletedDate: element.deletedDate,
            });
          });
        })
        .catch((e) => {
          console.log(e);
        });
    },
    getHistoryOfWorkPositions(employeeId) {
      WorkPositionService.getHistoryOfWorkPositions(employeeId)
        .then((response) => {
          this.editedItem.historyOfWorkPositions = [];
          response = response.data;
          response.forEach((element) => {
            element.startDate = element.startDate.split(" ")[0];
            element.endDate = element.endDate.split(" ")[0];
            this.editedItem.historyOfWorkPositions.push(element);
          });
          this.showEditDialog = true;
        })
        .catch((e) => {
          console.log(e);
        });
    },
    editItem(item) {
      this.editedIndex = this.employees.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.getHistoryOfWorkPositions(item.id);      
      this.editedItem.birthDate = this.editedItem.birthDate.split("T")[0];
      console.log(this.editedItem);
    },
    deleteItem(item) {
      this.editedIndex = this.positions.indexOf(item) + 1;
      this.deletedItem = {
        id: item.id,
        deletedDate: "",
      };
      this.dialogDelete = true;
    },
    deleteItemArchive() {
      this.deletedItem.deletedDate = new Date(
        Date.now() - new Date().getTimezoneOffset() * 60000
      )
        .toISOString()
        .substr(0, 10);
      EmployeeService.archiveEmployee(this.deletedItem)
        .then((response) => {
          console.log(response);
          this.retrieveEmployees();
        })
        .catch((e) => {
          console.log(e);
        });
      this.closeDelete();
    },
    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },
    deleteItemNoArchive() {
      this.deletedItem.deletedDate = "";
      EmployeeService.deleteEmployee(this.deletedItem)
        .then((response) => {
          console.log(response);
          this.retrieveEmployees();
        })
        .catch((e) => {
          console.log(e);
        });
      this.closeDelete();
    },
    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },
  },
};
</script>
