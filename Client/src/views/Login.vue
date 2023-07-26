<-- SPDX-License-Identifier: EUPL-1.2 -->
<template>
  <div id="login">
    <section class="hero">
      <div class="hero-body">
        <div class="container">
          <h2 class="is-size-3">
            Login
          </h2>
          <div
            ref="form"
            class="column is-half"
          >
            <section>
              <b-field label="User">
                <b-input
                  v-model="input.username"
                  required
                  validation-message="Scrivi qualcosa!"
                />
              </b-field>

              <b-field label="Password">
                <b-input
                  v-model="input.password"
                  type="password"
                  required
                  validation-message="Scrivi qualcosa!"
                />
              </b-field>
            </section>
            <section>
              <div class="buttons">
                <b-button
                  type="is-primary"
                  @click="login()"
                >
                  Ok!
                </b-button>
              </div>
            </section>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>

import api from "../api/api.js";
import helpers from "../helpers.js";

export default {
  name: "Login",
  data() {
    return {
      input: {
        username: "",
        password: "",
      },
    };
  },
  mounted: ()=> {
    localStorage.removeItem("user");
  },
  methods: {
    login() {
      api.login(this.input.username, this.input.password)
      .then((data)=>{
        let token =data.data.token;
        let role = data.data.role;
        let user = {username: this.input.username, token, role, direzione: data.data.direzione}
        localStorage.setItem("user", JSON.stringify(user));
        this.$router.replace({ name: "Fabbisogni" });
        // per effettuare il refresh della sidebar
        this.$emit("refreshuser",user);
        
      }) 
      .catch(()=> {
        helpers.error("Autenticazione fallita");
      })
    },
  },
};
</script>