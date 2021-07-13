<template>
  <div>
    <v-toolbar flat>
      <v-spacer></v-spacer>
      <v-btn
        color="dark-grey"
        dark
        class="mb-2"
        @click="showNewEmployeeDialog = true"
      >
        New Employee
      </v-btn>
    </v-toolbar>
    <v-simple-table>
      <template v-slot:default>
        <new-employee
          @createdNewEmployee="getNewData"
          :visible="showNewEmployeeDialog"
          @close="showNewEmployeeDialog = false"
        ></new-employee>
        <edit-employee
          @createdNewEmployee="getNewData"
          :visible="showEditDialog"
          @close="showEditDialog = false"
          :editedEmployee="editedItem"
        >
        </edit-employee>

        <detail-employee
          :visible="showDetailDialog"
          :detailedEmployee="editedItem"
          @close="showDetailDialog = false"
        ></detail-employee>

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
        <thead>
          <tr>
            <th class="text-left">Name</th>
            <th class="text-left">Work Position</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in employees" :key="item.fullName">
            <td @click="openEmployeeDetail(item)">{{ item.fullName }}</td>
            <td>{{ item.workPositionName }}</td>
            <td>
              <v-icon small class="mr-2" @click="editItem(item)">
                mdi-pencil
              </v-icon>
              <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
            </td>
          </tr>
        </tbody>
      </template>
    </v-simple-table>
  </div>
</template>


<script>
import EmployeeService from "../Services/EmployeeService";
import WorkPositionService from "../Services/WorkPositionService";
import NewEmployee from "../components/NewEmployee.vue";
import EditEmployee from "../components/EditEmployee.vue";
import DetailEmployee from "../components/DetailEmployee.vue";

export default {
  name: "Active",
  components: {
    NewEmployee,
    EditEmployee,
    DetailEmployee,
  },
  data: () => ({
    showEditDialog: false,
    editDialog: false,
    showDetailDialog: false,
    showNewEmployeeDialog: false,
    date: null,
    menu: false,
    positions: [],
    menu2: false,
    dialog: false,
    dialogDelete: false,
    employees: [],

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
      archivedDate: "",
    },
    defaultItem: {
      name: "",
      surname: "",
      birthDate: "",
      workPositionName: "",
      salary: 0,
      address: "",
      id: 0,
      historyOfWorkPositions: [],
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
    openEmployeeDetail(item) {
      this.editedItem = Object.assign({}, item);
      this.editedItem.birthDate = this.editedItem.birthDate.split("T")[0];
      WorkPositionService.getHistoryOfWorkPositions(item.id)
        .then((response) => {
          this.editedItem.historyOfWorkPositions = [];
          response = response.data;
          response.forEach((element) => {
            element.startDate = element.startDate.split(" ")[0];
            element.endDate = element.endDate.split(" ")[0];
            this.editedItem.historyOfWorkPositions.push(element);
          });
          this.showDetailDialog = true;
        })
        .catch((e) => {
          console.log(e);
        });
    },
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
              archivedDate: element.archivedDate,
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
    },
    deleteItem(item) {
      this.deletedItem = {
        id: item.id,
        archivedDate: "",
      };
      this.dialogDelete = true;
    },
    deleteItemArchive() {
      this.deletedItem.archivedDate = new Date(
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
      });
    },
    deleteItemNoArchive() {
      this.deletedItem.archivedDate = "";
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
      });
    },
  },
};
</script>