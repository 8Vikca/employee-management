<template>
  <v-dialog v-model="show" max-width="600px">
    <v-card>
      <v-card-title>
        <span class="text-h5"> Employee Detail</span>
      </v-card-title>
      <v-form>
        <v-container>
          <v-row>
            <v-col cols="12" sm="6" md="4">
              <v-text-field
                v-model="detailedEmployee.name"
                label="Name"
                readonly
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-text-field
                v-model="detailedEmployee.surname"
                label="Surname"
                readonly
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-text-field
                v-model="detailedEmployee.address"
                label="Address"
                readonly
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-text-field
                v-model="detailedEmployee.salary"
                label="Salary"
                type="number"
                readonly
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-select
                :items="positions"
                label="Work Position"
                v-model="detailedEmployee.workPositionName"
                :item-text="detailedEmployee.workPositionName"
                readonly
              ></v-select>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-text-field
                label="Birthdate"
                v-model="detailedEmployee.birthDate"
                prepend-icon="mdi-calendar"
                readonly
              ></v-text-field>
            </v-col>

            <template>
              <v-data-table
                height="150px"
                :headers="headers"
                fixed-header
                :items="detailedEmployee.historyOfWorkPositions"
                :hide-default-footer="true"
                class="elevation-1"
              ></v-data-table>
            </template>
          </v-row>
        </v-container>
      </v-form>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click.stop="show = false">
          OK
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import WorkPositionService from "../Services/WorkPositionService";

export default {
  props: ["visible", "detailedEmployee"],
  name: "DetailEmployee",
  data: () => ({
    headers: [
      { text: "Work Position", sortable: false, value: "workPositionName" },
      { text: "StartDate", value: "startDate" },
      { text: "EndDate", value: "endDate" },
    ],
    activePicker: null,
    nameRules: [
      (v) => !!v || "Name is required",
      (v) => (v && v.length <= 10) || "Name must be less than 10 characters",
    ],
    menu: false,
    positions: [],
    menu2: false,
    dialog: false,
    dataRendered: false,
  }),
  watch: {
    menu(val) {
      val && setTimeout(() => (this.activePicker = "YEAR"));
    },
  },
  computed: {
    show: {
      get() {
        return this.visible;
      },
      set(value) {
        if (!value) {
          this.$emit("close");
        }
      },
    },
  },
  created() {
    this.retrieveWorkPositions();
  },
  methods: {
    retrieveWorkPositions() {
      WorkPositionService.getAllPositions()
        .then((response) => {
          response = response.data;
          response.forEach((element) => {
            this.positions.push(element.workPositionName);
          });
        })
        .catch((e) => {
          console.log(e);
        });
    },
  },
};
</script>


