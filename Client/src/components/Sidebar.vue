<-- SPDX-License-Identifier: EUPL-1.2 -->
<template>
    <div class="sidebar-page">
    <section
      class="sidebar-layout"
      style="color: #06c;"
    >
      <div>
        <b-menu class="is-custom-mobile">
          <b-menu-list label="Gestione fabbisogno">
            <b-menu-item
              tag="router-link"
              to="/"
              icon="format-list-numbered-rtl"
              label="Inserimento"
            />
          </b-menu-list>
          <b-menu-list
            v-if="ruolo == 1"
            label="Liste di valori"
          >
            <b-menu-item
              tag="router-link"
              to="/mansioni"
              icon="briefcase"
              label="Mansioni"
            />
            <b-menu-item
              tag="router-link"
              to="/motivi"
              icon="account-question"
              label="Motivi richiesta"
            />
            <b-menu-item
              tag="router-link"
              to="/profili"
              icon="binoculars"
              label="Profili"
            />
            <b-menu-item
              tag="router-link"
              to="/utenti"
              icon="account-multiple"
              label="Utenti"
            />
          </b-menu-list>

          <b-menu-list label="Altro">
            <b-menu-item
              tag="router-link"
              icon="logout"
              label="Logout"
              to="/login"
              replace
              @click.native="logout()"
            />
          </b-menu-list>
        </b-menu>
        <span class="is-size-7 is-italic">
          Versione {{ releaseVersion }} {{ releaseDate }}</span>
      </div>
    </section>
  </div>
</template>

<script>
const { version } = require("../../package.json");
const { date } = require("../config/builddate.json");

export default {
  name: "Sidebar",
  props: ["user"],
  data() {
    return {
      releaseVersion: "",
      releaseDate: "",
      ruolo: 0,
    };
  },
  watch: {
    user: function (newVal) {
      this.ruolo = newVal.role;
    },
  },
  mounted() {
    this.releaseVersion = version;
    this.releaseDate = date;
    let user = localStorage.getItem("user");
    this.ruolo = user ? JSON.parse(user).role : null;
    
    
  },
  methods: {
    logout() {
      localStorage.removeItem("user");
    }
  },
};
</script>

<style lang="scss">
.sidebar-page {
  display: flex;
  flex-direction: column;
  width: 100%;
  min-height: 100%;
  .sidebar-layout {
    display: flex;
    flex-direction: row;
    min-height: 100%;
  }
}
</style>