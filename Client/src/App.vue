<-- SPDX-License-Identifier: EUPL-1.2 -->
<template>
  <div id="app">
    <div class="container is-fluid">
      <div
        class="columns is-flex is-vcentered"
        style="background-color: #152f52"
      >
        <div class="column is-2">
          <img
            src="/logo.png"
            alt="Fabbisogno personale"
            width="50%"
          >
        </div>
        <div class="column is-10">
          <h1 class="title is-size-1 has-text-white">
            Fabbisogno del Personale
          </h1>
        </div>
      </div>
      <div class="columns">
        <div
          class="column is-2 has-text-white"
          style="background-color: #D1D1D1"
        >
          <Sidebar :user="user" />
        </div>
        <div
          class="column is-10"
          style="background-color: #e5e5e5"
        >
          <router-view @refreshuser="setUser" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import Sidebar from "@/components/Sidebar";

export default {
  name: "App",
  components: { Sidebar },
  data() {
    return {
      user: null,
    };
  },
  mounted() {
    console.log(this.user);
  },
  created(){
    window.addEventListener('beforeunload', this.leaving) 
  },
  methods: {
    setUser(x) {
      console.info(x);
      this.user = x;
    },
    leaving() {
      // alla chiusura del tab/browser si disconnette l'utente
      localStorage.removeItem("user");
    },
  },
};
</script>

<style lang="css">

.container {
    padding-top: 10px;
    max-width: 1800px;
}
</style>

