<template>
  <v-dialog v-model="dialog" max-width="600px">
    <template v-slot:activator="{ on, attrs }">
      <v-btn color="dark-grey" dark class="mb-2" v-bind="attrs" v-on="on">
        New Employee
      </v-btn>
    </template>
    <v-card>
      <v-card-title>
        <span class="text-h5"> New Employee</span>
      </v-card-title>
      <v-card-text>
        <v-form v-model="valid">
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  v-model="newEmployee.name"
                  label="Name"
                  :counter="10"
                  :rules="nameRules"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  v-model="newEmployee.surname"
                  label="Surname"
                  :counter="10"
                  :rules="nameRules"
                  required
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  v-model="newEmployee.address"
                  label="Address"
                  :counter="15"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-text-field
                  v-model="newEmployee.salary"
                  label="Salary"
                  type="number"
                  :rules="[(v) => !!v || 'Salary is required']"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="4">
                <v-select
                  :items="positions"
                  label="Work Position"
                  v-model="newEmployee.workPositionName"
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
                      v-model="newEmployee.birthDate"
                      label="Birthdate"
                      prepend-icon="mdi-calendar"
                      readonly
                      v-bind="attrs"
                      v-on="on"
                      :rules="[(v) => !!v || 'Birthdate is required']"
                      required
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="newEmployee.birthDate"
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
              <v-col cols="12" sm="6" md="4">
                <v-menu
                  v-model="menu2"
                  :close-on-content-click="false"
                  :nudge-right="40"
                  transition="scale-transition"
                  offset-y
                  min-width="auto"
                >
                  <template v-slot:activator="{ on, attrs }">
                    <v-text-field
                      v-model="newEmployee.onBoardDate"
                      label="Start Date"
                      prepend-icon="mdi-calendar"
                      readonly
                      v-bind="attrs"
                      v-on="on"
                      :rules="[(v) => !!v || 'Start date is required']"
                      required
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="newEmployee.onBoardDate"
                    @input="menu2 = false"
                    :min="
                      new Date(
                        Date.now() - new Date().getTimezoneOffset() * 60000
                      )
                        .toISOString()
                        .substr(0, 10)
                    "
                  ></v-date-picker>
                </v-menu>
              </v-col>
            </v-row>
          </v-container>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click="close"> Cancel </v-btn>
        <v-btn
          :disabled="!valid"
          color="blue darken-1"
          text
          @click="createNewEmployee">
           Create
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import EmployeeService from "../Services/EmployeeService";
import WorkPositionService from '../Services/WorkPositionService';

export default {
  name: "NewEmployee",
  data: () => ({
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
    newEmployee: {
      name: "",
      surname: "",
      address: "",
      birthDate: "",
      onBoardDate: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      workPositionName: "",
      salary: "",
    },
    defaultItem: {
      name: "",
      surname: "",
      address: "",
      birthDate: "",
      onBoardDate: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      workPositionName: "",
      salary: "",
    },
  }),

  watch: {
    menu (val) {
        val && setTimeout(() => (this.activePicker = 'YEAR'))
      },
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },

  created() {
    this.retrieveWorkPositions();
  },

  methods: {
    saveBirthDate(date) {
      this.$refs.menu.save(date);
    },
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

    createNewEmployee() {
      this.close();
      EmployeeService.createNewEmployee(this.newEmployee).then(
        (response) => {
          console.log(response);
          this.$emit('createdNewEmployee', 'true') 
          this.newEmployee = Object.assign({}, this.defaultItem);
        },
        (error) => {
          console.log(error);
        }
      );
    },
  },
};
</script>