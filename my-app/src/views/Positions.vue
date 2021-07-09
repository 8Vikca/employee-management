<template>
  <v-data-table :headers="headers" :items="positions" class="elevation-1">
    <template v-slot:top>
        <!-- <edit-employee :visible="showEditDialog" @close="showEditDialog=false" :editedEmployee="editedItem" @createdNewEmployee="getNewData"> </edit-employee> -->
      <v-toolbar flat>
        <v-spacer></v-spacer>
        <new-work-position @createdNewEmployee="getNewData"></new-work-position>
        
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">
              Do you want to delete this position?
            </v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeDelete">No</v-btn>
              <v-btn color="red darken-1" text @click="deleteItemConfirm">
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
      <v-btn color="primary" @click="retrievePositions"> Reset </v-btn>
    </template>
  </v-data-table>
</template>

<script>

import EmployeeService from "../Services/EmployeeService";
import NewWorkPosition from "../components/NewWorkPosition.vue";

  export default {
    name: 'Positions',
    components: {
      NewWorkPosition
    },
    data: () => ({
    showEditDialog: false,
    positions: [],
    dialogDelete: false,
    headers: [
      { text: "Name", align: "start", value: "workPositionName" },
      { text: "Actions", value: "actions", sortable: false },
    ],
    editedIndex: -1,
    defaultItem: {
      workPositionId: -1,
      workPositionName: "",
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
    this.retrievePositions();
  },

  methods: {
    getNewData(value) {
      console.log(value);
      this.retrievePositions();
    },
    retrievePositions() {
        EmployeeService.getAllPositions().then(response => {
          this.positions = response.data;
        })
        .catch(e => {
          console.log(e);
        });
      },
    deleteItem(item) {
      this.editedIndex = this.positions.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },
    deleteItemConfirm() {
      this.desserts.splice(this.editedIndex, 1);
      this.closeDelete();
    },
    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
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