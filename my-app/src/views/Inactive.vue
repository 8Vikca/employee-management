<template>
  <v-simple-table>
    <template v-slot:default>
      <detail-employee
        :visible="showDetailDialog"
        :detailedEmployee="editedItem"
        @close="showDetailDialog = false"
      ></detail-employee>

     <v-dialog v-model="dialogDelete" max-width="450px">
          <v-card>
            <v-card-title class="text-h5">
              Do you want to delete this employee permanently?
            </v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeDelete"
                >No</v-btn
              >
              <v-btn color="red darken-1" text @click="deleteItemPermanently">
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
          <th class="text-left">Archived at</th>
          <th class="text-left">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in employees" :key="item.fullName">
          <td @click="openEmployeeDetail(item)">{{ item.fullName }}</td>
          <td>{{ item.workPositionName }}</td>
          <td>{{ item.archivedDate }}</td>
          <td>
            <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
          </td>
        </tr>
      </tbody>
    </template>
  </v-simple-table>
</template>


<script>
import EmployeeService from "../Services/EmployeeService";
import WorkPositionService from "../Services/WorkPositionService";
import DetailEmployee from "../components/DetailEmployee.vue";

export default {
  name: "Inactive",
  components: {
    DetailEmployee,
  },
  data: () => ({
    showDetailDialog: false,
    date: null,
    menu: false,
    positions: [],
    menu2: false,
    dialog: false,
    dialogDelete: false,
    headers: [
      { text: "Name", align: "start", value: "fullName" },
      { text: "Position", value: "workPositionName" },
      { text: "Archived at", value: "archivedDate" },
      { text: "Actions", value: "actions", sortable: false },
    ],
    employees: [],

    editedIndex: -1,
    deletedItem: {
      id: 0,
      archivedDate: "",
    },
    defaultItem: {
      name: "",
      surname: "",
      position: "",
      lastPositions: "",
    },
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
    retrieveEmployees() {
      EmployeeService.getAllInactiveEmployees()
        .then((response) => {
          response = response.data;
          this.employees = [];
          response.forEach((element) => {
            element.archivedDate = element.archivedDate.split("T")[0];
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
    deleteItem(item) {
      this.editedIndex = this.positions.indexOf(item) + 1;
      this.deletedItem = {
        id: item.id,
        archivedDate: "",
      };
      this.dialogDelete = true;
    },
    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },
    deleteItemPermanently() {
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
        this.editedIndex = -1;
      });
    },
  },
};
</script>


  <!-- <v-data-table :headers="headers" :items="employees" class="elevation-1">
    <template v-slot:top>
      <v-toolbar flat>
        <v-spacer></v-spacer>

        <v-dialog v-model="dialogDelete" max-width="450px">
          <v-card>
            <v-card-title class="text-h5">
              Do you want to delete this employee permanently?
            </v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeDelete"
                >No</v-btn
              >
              <v-btn color="red darken-1" text @click="deleteItemPermanently">
                Yes
              </v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>

    <template v-slot:[`item.actions`]="{ item }">
      <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
    </template>
    <template v-slot:no-data>
      <v-btn color="primary" @click="retrieveEmployees"> Reset </v-btn>
    </template>
  </v-data-table> -->
