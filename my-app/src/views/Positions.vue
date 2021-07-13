<template>
  <v-data-table :headers="headers" :items="positions" class="elevation-1">
    <template v-slot:top>
      <v-toolbar flat>
        <v-spacer></v-spacer>
        <new-work-position
          @createdNewWorkPosition="getNewData"
        ></new-work-position>

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
import WorkPositionService from "../Services/WorkPositionService";
import NewWorkPosition from "../components/NewWorkPosition.vue";

export default {
  name: "Positions",
  components: {
    NewWorkPosition,
  },
  data: () => ({
    positions: [],
    dialogDelete: false,
    headers: [
      { text: "Name", align: "start", value: "workPositionName" },
      { text: "Actions", value: "actions", sortable: false },
    ],
    editedItem: {
      workPositionId: -1,
      workPositionName: "",
    },
    defaultItem: {
      Id: -1,
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
      WorkPositionService.getAllPositions()
        .then((response) => {
          response = response.data;
          this.positions = [];
          response.forEach((element) => {
            this.positions.push({
              workPositionName: element.workPositionName,
              workPositionId: element.id,
            });
          });
        })
        .catch((e) => {
          console.log(e);
        });
    },
    deleteItem(item) {
      console.log(item);
      this.editedItem = {
        id: item.id,
        workPositionName: item.workPositionName,
      };
      this.dialogDelete = true;
    },
    deleteItemConfirm() {
      WorkPositionService.deleteWorkPosition(this.editedItem)
        .then((response) => {
          console.log(response);
          this.retrievePositions();
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
    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
      });
    },
  },
};
</script>