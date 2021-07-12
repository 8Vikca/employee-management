<template>
  <v-dialog v-model="dialog" max-width="400px">
    <template v-slot:activator="{ on, attrs }">
      <v-btn color="dark-grey" dark class="mb-2" v-bind="attrs" v-on="on">
        New Position
      </v-btn>
    </template>
    <v-card>
      <v-card-title>
        <span class="text-h5"> New Work Position</span>
      </v-card-title>
      <v-card-text>
        <v-form v-model="valid">
          <v-container>
            <v-row>
              <v-col cols="12" sm="12" md="12">
                <v-text-field
                class="shrink"
                  v-model="newWorkPosition.workPositionName"
                  label="Name"
                  :counter="15"
                  :rules="nameRules"
                  required
                ></v-text-field>
              </v-col>
            </v-row>
          </v-container>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click="close" > Cancel </v-btn>
        <v-btn
          :disabled="!valid"
          color="blue darken-1"
          text
          @click="createNewWorkPosition">
           Create
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import WorkPositionService from "../Services/WorkPositionService";

export default {
  name: "NewWorkPosition",
  data: () => ({
    nameRules: [
      (v) => !!v || "Name is required",
      (v) => (v && v.length <= 15) || "Name must be less than 15 characters",
    ],
    valid: false,
    positions: [],
    dialog: false,
    newWorkPosition: {
      workPositionName: ""
  }}),

  watch: {
    dialog(val) {
      val || this.close();
    },
    // dialogDelete(val) {
    //   val || this.closeDelete();
    // },
  },

  created() {
  },

  methods: {
    // deleteItemConfirm() {
    //   this.desserts.splice(this.editedIndex, 1);
    //   this.closeDelete();
    // },
    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    // closeDelete() {
    //   this.dialogDelete = false;
    //   this.$nextTick(() => {
    //     this.editedItem = Object.assign({}, this.defaultItem);
    //     this.editedIndex = -1;
    //   });
    // },

    createNewWorkPosition() {
      this.close();
      WorkPositionService.createNewWorkPosition(this.newWorkPosition).then(
        (response) => {
          console.log(response);
          this.$emit('createdNewWorkPosition', 'true');
          this.newWorkPosition.workPositionName = "";
        },
        (error) => {
          console.log(error);
        }
      );
    },
  },
};
</script>