<template>
  <v-data-table :headers="headers" :items="employees" class="elevation-1">
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
  </v-data-table>
</template>


<script>
import EmployeeService from "../Services/EmployeeService";

export default {
  name: "Inactive",
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
      { text: "Archived", value: "deletedDate" },
      { text: "Actions", value: "actions", sortable: false },
    ],
    employees: [],

    editedIndex: -1,
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
    retrieveEmployees() {
      EmployeeService.getAllInactiveEmployees()
        .then((response) => {
          response = response.data;
          this.employees = [];
          response.forEach((element) => {
            element.deletedDate = element.deletedDate.split("T")[0];
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
    deleteItem(item) {
      this.editedIndex = this.positions.indexOf(item) + 1;
      this.deletedItem = {
        id: item.id,
        deletedDate: "",
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
