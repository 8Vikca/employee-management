<template>
  <v-dialog v-model="show" max-width="600px">
    <v-card>
      <v-card-title>
        <span class="text-h5"> Edit Employee</span>
      </v-card-title>
      <v-form v-model="valid">
        <v-container>
          <v-row>
            <v-col cols="12" sm="6" md="4">
              <v-text-field
                v-model="editedEmployee.name"
                label="Name"
                :counter="10"
                :rules="nameRules"
                required
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-text-field
                v-model="editedEmployee.surname"
                label="Surname"
                :counter="10"
                :rules="nameRules"
                required
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-text-field
                v-model="editedEmployee.address"
                label="Address"
                :counter="15"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-text-field
                v-model="editedEmployee.salary"
                label="Salary"
                type="number"
                :rules="[(v) => !!v || 'Salary is required']"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-select
                :items="positions"
                label="Work Position"
                v-model="editedEmployee.workPositionName"
                :item-text="editedEmployee.workPositionName"
                :rules="[(v) => !!v || 'Work position is required']"
                required
              ></v-select>
            </v-col>
            <v-col cols="12" sm="6" md="4">
              <v-menu
                ref="menu"
                v-model="menu"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="auto"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    label="Birthdate"
                    v-model="editedEmployee.birthDate"
                    prepend-icon="mdi-calendar"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                    :rules="[(v) => !!v || 'Birthdate is required']"
                    required
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="editedEmployee.birthDate"
                  :active-picker.sync="activePicker"
                  :max="
                    new Date(
                      Date.now() - new Date().getTimezoneOffset() * 60000
                    )
                      .toISOString()
                      .substr(0, 10)
                  "
                  min="1950-01-01"
                  @change="saveBirthDate"
                ></v-date-picker>
              </v-menu>
            </v-col>

            <template>
              <v-data-table
              height="150px"
                :headers="headers"
                fixed-header
                :items="editedEmployee.historyOfWorkPositions"
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
          Cancel
        </v-btn>
        <v-btn
          :disabled="!valid"
          color="blue darken-1"
          text
          @click="editEmployee"
        >
          Save Changes
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import Vue from "vue";
import EmployeeService from "../Services/EmployeeService";
import WorkPositionService from "../Services/WorkPositionService";

export default {
  name: "EditEmployee",
  props: ["visible", "editedEmployee"],
  data: () => ({
    headers: [
          { text: 'Work Position', sortable: false, value: 'workPositionName' },
          { text: 'StartDate', value: 'startDate' },
          { text: 'EndDate', value: 'endDate' },
        ],
    activePicker: null,
    nameRules: [
      (v) => !!v || "Name is required",
      (v) => (v && v.length <= 10) || "Name must be less than 10 characters",
    ],
    valid: false,
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
    editEmployee() {
      this.show = false;
      Vue.delete(this.editedEmployee, "fullName");
      Vue.delete(this.editedEmployee, "deletedDate");
      EmployeeService.editEmployee(this.editedEmployee).then(
        (response) => {
          console.log(response);
          this.$emit("createdNewEmployee", "true");
        },
        (error) => {
          console.log(error);
        }
      );
    },
    saveBirthDate(date) {
      this.$refs.menu.save(date);
    },
  },
};
</script>
