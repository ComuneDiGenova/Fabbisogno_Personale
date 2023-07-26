<-- SPDX-License-Identifier: EUPL-1.2 -->
<template>
  <section class="hero">
    <div class="hero-body">
      <div class="container">
        <h1 class="title">
          Utenti
        </h1>
        <b-tabs v-model="selectedTab">
          <b-tab-item label="Tabella">
            <div class="buttons">
              <b-button
                type="is-primary"
                @click="doNew"
              >
                Nuovo
              </b-button>
            </div>
            <div style="cursor: pointer">
              <b-table
                :data="rowData"
                :columns="columns"
                :striped="true"
                :selected.sync="selected"
                @select="select"
                @click="rowClicked"
              >
                <b-input
                  slot="searchable"
                  v-model="props.filters[props.column.field]"
                  slot-scope="props"
                  placeholder="Cerca..."
                  icon="magnify"
                  size="is-small"
                />
              </b-table>
            </div>
          </b-tab-item>

          <b-tab-item
            label="Azioni"
            disabled
          >
            <div
              ref="form"
              class="column is-half"
            >
              <section>
                <div class="buttons">
                  <b-button
                    type="is-primary"
                    @click="doBack"
                  >
                    Torna indietro
                  </b-button>
                  <b-button
                    type="is-info"
                    @click="doEdit"
                  >
                    Salva
                  </b-button>
                </div>
              </section>
              <section>
                <span
                  v-if="newItem.Id"
                  class="subtitle"
                >Id: {{ newItem.ID }}</span>
                <b-field label="Matricola">
                  <b-input
                    ref="iMat"
                    v-model="newItem.MATRICOLA"
                    required
                    validation-message="Scrivi qualcosa!"
                    autocomplete="off"
                  />
                </b-field>

                
                <b-field label="Nome">
                  <b-input
                    ref="iNome"
                    v-model="newItem.NOME"
                    required
                    validation-message="Scrivi qualcosa!"
                    autocomplete="off"
                  />
                </b-field>
                <b-field label="Cognome">
                  <b-input
                    ref="iCognome"
                    v-model="newItem.COGNOME"
                    required
                    validation-message="Scrivi qualcosa!"
                    autocomplete="off"
                  />
                </b-field>

                <b-field label="Direzione">
                  <b-input
                    ref="iDir"
                    v-model="newItem.DIREZIONE"
                    required
                    validation-message="Scrivi qualcosa!"
                    autocomplete="off"
                  />
                </b-field>
                <b-field
                  ref="iRuolo"
                  v-model="newItem.DIREZIONE"
                  label="Ruolo"
                  required
                >
                  <b-select v-model="newItem.RUOLO">
                    <option
                      v-for="cat in ruoli"
                      :key="cat.Id"
                      :value="cat.Id"
                    >
                      {{ cat.Name }}
                    </option>
                  </b-select>
                </b-field>
                <b-field label="Attivo">
                  <b-checkbox
                    v-model="newItem.ATTIVO_B"
                  />
                </b-field>
                <b-field :label="newItem.ID?'Password (lascia in bianco per non variarla)':'Password'">
                  <b-input
                    ref="input1"
                    v-model="newItem.PASSWORD"
                    type="password"
                    autocomplete="off"
                  />
                </b-field>
              </section>
            </div>
          </b-tab-item>
        </b-tabs>
      </div>
    </div>
  </section>
</template>

<script>
import api from "../api/api.js";
import helpers from "../helpers.js";

const emptyVal = { ID: 0, NOME: null, COGNOME: null, RUOLO: 2, ATTIVO_B: true, MATRICOLA:null,DIREZIONE:null, PASSWORD: null };

export default {
  name: "Utenti",
  data() {
    return {
      selectedRow: null,
      newItem: emptyVal,
      rowData: [],
      selected: null,
      selectedTab: 0,
      ruoli: [
        { Id: 1, Name: "ADMIN" },
        { Id: 2, Name: "USER" },
      ],
      columns: [
        {
          field: "ID",
          label: "id",
          width: "40",
          numeric: true,
        },
        {
          field: "MATRICOLA",
          label: "Categoria",
          searchable: true,
          sortable: true,
          width: "40",
        },
        {
          field: "NOME",
          label: "Nome",
          searchable: true,
          sortable: true,
        },
        {
          field: "COGNOME",
          label: "Cognome",
          searchable: true,
          sortable: true,
        },
        {
          field: "DIREZIONE",
          label: "Direzione",
          searchable: true,
          sortable: true,
        },
        {
          field: "ROLE_NAME.Name",
          label: "Ruolo",
          searchable: true,
          sortable: true,
        },
        {
          field: "ATTIVO_B",
          label: "Attivo",
          searchable: true,
          sortable: true,
        },
      ],
    };
  },
  mounted() {
    const loading = this.$buefy.loading.open();
    api.utenti.getAll().then((rows) => {
      this.rowData = this.formatRow(rows.data);
      loading.close();
    });
  },
  methods: {
      formatRow(rows) {
      rows.forEach((r) => {
        r.ATTIVO_B = (r.ATTIVO==1);
        r.ROLE_NAME = this.ruoli.find((x)=>{return x.Id==r.RUOLO});
        
        
      });
      return rows;
    },
    doNew() {
      this.newItem = Object.assign({}, emptyVal);
      this.selectedTab = 1;
    },
    doBack() {
      this.newItem = emptyVal;
      this.selectedTab = 0;
      this.selected = null;
    },
    doEdit() {
      let val = this.$refs["iMat"].checkHtml5Validity() &&
      this.$refs["iNome"].checkHtml5Validity() &&
      this.$refs["iCognome"].checkHtml5Validity() &&
      this.$refs["iDir"].checkHtml5Validity();
      if (!val) {
        return;
      }
      if (!this.newItem.ID && !this.newItem.PASSWORD) {
        helpers.error ("Indicare una password");
        return;
      }
      let apiMethod =
      this.newItem.ID != 0 ? api.utenti.update : api.utenti.create;
      const loading = this.$buefy.loading.open();
      apiMethod(this.newItem)
        .then(() => {
          return api.utenti.getAll();
        })
        .then((rows) => {
          this.rowData = this.formatRow(rows.data);
          helpers.ok("Utente correttamente salvato");
          this.newItem = emptyVal;
          this.selectedTab = 0;
          loading.close();
        })
        .catch((err) => {
          helpers.error("Errore! " + err);
          loading.close();
        });
    },
    rowClicked(row) {
      if (row.ID == this.newItem.ID) {
        this.selected = null;
        this.newItem = emptyVal;
      } else {
        this.selectedTab = 1;
      }
    },
    select(row) {
      this.newItem = row;
    },
  },
};
</script>

<style>
.buttons {
  padding-top: 10px;
}
</style>