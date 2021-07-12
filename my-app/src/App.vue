<template>
  <v-app>
    <v-app-bar app dark>
      <v-btn text router-link to="Active"> Active Employees </v-btn>
      <v-btn text router-link to="Archived"> Archived Employees </v-btn>
      <v-btn text router-link to="Positions"> Work Positions </v-btn>
      <v-spacer></v-spacer>
    </v-app-bar>

    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: "App",
  data: () => ({
    //
  }),
};
</script>
©