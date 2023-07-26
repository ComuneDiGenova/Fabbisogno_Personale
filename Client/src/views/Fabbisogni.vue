<-- SPDX-License-Identifier: EUPL-1.2 -->
<template>
  <section class="hero">
    <div class="hero-body">
      <div class="container">
        <h1 class="title">
          Gestione fabbisogni
        </h1>
        <b-tabs
          v-model="selectedTab"
          type="is-boxed"
        >
          <b-tab-item label="Tabella">
            <div class="buttons">
              <b-button
                v-if="!isAdmin"
                type="is-primary"
                @click="doNew"
              >
                Nuovo
              </b-button>
              <b-button
                v-if="!isAdmin"
                type="is-success"
                @click="doConfirmAll"
              >
                Conferma
              </b-button>
            </div>
            <div style="cursor: pointer">
              <b-table
                :data="rowData"
                :columns="columns"
                :striped="true"
                :paginated="true"
                :per-page="15"
                :pagination-simple="false"
                :selected.sync="selected"
                :checked-rows.sync="checkedRows"
                :checkable="!isAdmin"
                checkbox-position="left"
                :is-row-checkable="(row) => row.STATO_ID == 0"
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
            <div class="buttons">
              <b-button
                type="is-primary"
                @click="doBack"
              >
                Torna indietro
              </b-button>

              <b-button
                v-if="user.role == 1 || newItem.STATO_ID == 0"
                type="is-info"
                @click="doEdit"
              >
                Salva
              </b-button>

              <b-button
                v-if="newItem.STATO_ID == 0 && newItem.ID && !isAdmin"
                type="is-warning"
                @click="doChangeState(1)"
              >
                Conferma
              </b-button>
              <b-button
                v-if="newItem.STATO_ID == 1 && isAdmin"
                type="is-warning"
                @click="doChangeState(3)"
              >
                Marca come analizzato
              </b-button>
              <b-button
                v-show="newItem.ID != 0 && newItem.STATO_ID == 0"
                type="is-danger"
                @click="doDelete"
              >
                Cancella il record
              </b-button>
              <b-button
                v-show="newItem.STATO_ID == 1"
                type="is-danger"
                @click="doChangeState(2)"
              >
                Annulla il record
              </b-button>
            </div>
            <fieldset :disabled="disabled">
              <div class="columns">
                <div
                  ref="form"
                  class="column is-full"
                >
                  <b-field label="Direzione">
                    <b-input
                      ref="input1"
                      v-model="newItem.DIREZIONE"
                      required
                      disabled
                      validation-message="Scrivi qualcosa!"
                    />
                  </b-field>
                </div>
              </div>

              <!-- <div class="columns">
                
              </div> -->
              
              <div class="columns">
                <div class="column is-2">
                  <b-field label="Anno">
                    <b-select
                      v-model="newItem.ANNO"
                      expanded
                    >
                      <option
                        v-for="anno in anni"
                        :key="anno"
                        :value="anno"
                      >
                        {{ anno }}
                      </option>
                    </b-select>
                  </b-field>
                </div>

                <div class="column is-2">
                  <b-field label="Categorie">
                    <b-select
                      ref="iCat"
                      v-model="newItem.CATEGORIA"
                      expanded
                      required
                      validation-message="Inserisci un valore!"
                      @input="doChangeCategory()"
                    >
                      <option
                        v-for="cat in categorie"
                        :key="cat"
                        :value="cat"
                      >
                        {{ cat }}
                      </option>
                    </b-select>
                  </b-field>
                </div>
                
                <div class="column is-4">
                  <b-field label="Profilo">
                    <b-select
                      ref="iProf"
                      v-model="newItem.PROFILO_ID"
                      required
                      expanded
                    >
                      <option
                        v-for="pr in profili.filter((x) => {
                          return x.CATEGORIA == newItem.CATEGORIA;
                        })"
                        :key="pr.ID"
                        :value="pr.ID"
                      >
                        {{ pr.NOME }}
                      </option>
                    </b-select>
                  </b-field>
                </div>
              </div>

              <div class="columns">
                <div class="column is-4">
                  <b-field label="Mansione">
                    <b-select
                      ref="iMans"
                      v-model="newItem.MANSIONE_ID"
                      expanded
                    >
                      <option
                        v-for="mans in mansioni"
                        :key="mans.ID"
                        :value="mans.ID"
                      >
                        {{ mans.NOME }}
                      </option>
                    </b-select>
                  </b-field>
                </div>

                <div class="column is-4">
                  <b-field label="Motivo richiesta">
                    <b-select
                      ref="input1"
                      v-model="newItem.MOTIVO_RICHIESTA_ID"
                      required
                      expanded
                    >
                      <option
                        v-for="m in motiviRichiesta"
                        :key="m.ID"
                        :value="m.ID"
                      >
                        {{ m.NOME }}
                      </option>
                    </b-select>
                  </b-field>
                </div>

                <div class="column is-2">
                  <b-field label="Unità">
                    <b-input
                      ref="iUnità"
                      v-model="newItem.UNITA"
                      required
                      type="number"
                      min="1"
                    />
                  </b-field>
                </div>
              </div>

              <div class="columns">
                <div class="column is-10">
                  <b-field label="Profilo formativo">
                    <b-input
                      ref="iProfForm"
                      v-model="newItem.PROFILO_FORMATIVO"
                      maxlength="100"
                      validation-message="Scrivi qualcosa!"
                      type="textarea"
                    />
                  </b-field>
                </div>


                <div class="column is-2">
                  <b-field label="Stato">
                    <b-input
                      v-model="newItem.STATO"
                      disabled
                    />
                  </b-field>
                </div>
              </div>

              <div class="columns">
                <div class="column is-12">
                  <b-field label="Annotazioni">
                    <b-input
                      ref="iProfForm"
                      v-model="newItem.ANNOTAZIONI"
                      maxlength="100"
                      validation-message="Scrivi qualcosa!"
                      type="textarea"
                    />
                  </b-field>
                </div>
              </div>
            </fieldset>
            <div class="columns">
              <div class="column is-half">
                <br>
                <b-field v-if="newItem.DATA_INS">
                  <b-tag
                    rounded
                    size="is-medium"
                  >
                    Inserito da {{ newItem.FULL_UTENTE_INS }} in data {{ newItem.DATA_INS }}
                  </b-tag>
                </b-field>
                <b-field v-if="newItem.DATA_CONF">
                  <b-tag
                    rounded
                    size="is-medium"
                  >
                    Confermato da {{ newItem.FULL_UTENTE_CONF }} in data {{ newItem.DATA_CONF }}
                  </b-tag>
                </b-field>
                <b-field v-if="newItem.DATA_ANN">
                  <b-tag
                    rounded
                    size="is-medium"
                  >
                    Annullato da {{ newItem.FULL_UTENTE_ANN }} in data {{ newItem.DATA_ANN }}
                  </b-tag>
                </b-field>
                <b-field v-if="newItem.DATA_ANALIZZATO">
                  <b-tag
                    rounded
                    size="is-medium"
                  >
                    Marcato come analizzato da {{ newItem.FULL_UTENTE_ANALIZZATO }} in data {{ newItem.DATA_ANALIZZATO }}
                  </b-tag>
                </b-field>
              </div>
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

let emptyVal = {
  ID: 0,
  DIREZIONE: null,
  MANSIONE_ID: 0,
  ANNO: 2022,
  UNITA: 1,
  CATEGORIA: null,
  PROFILO_FORMATIVO: "",
  MOTIVO_RICHIESTA_ID: 0,
  STATO_ID: 0,
  PROFILO_ID: 0,
  ANNOTAZIONI: ""
};

export default {
  name: "Fabbisogni",
  data() {
    return {
      selectedRow: null,
      checkedRows: [],
      newItem: emptyVal,
      rowData: [],
      selected: null,
      mansioni: [],
      motiviRichiesta: [],
      profili: [],
      user: {}, // Role == 1 => Amministratore, 2 => Utente
      disabled: false,
      isAdmin: false,
      selectedTab: 0,
      anni: ["2023", "2024","2025"], // TODO: calcolarli in base all'anno corrente
      categorie: ["B", "C", "D", "Dir"],
      stati: ["In corso", "Confermato", "Annullato", "Analizzato"],
      columns: [
        {
          field: "DIREZIONE",
          label: "Direzione",
          searchable: true,
          sortable: true,
        },
        {
          field: "ANNO",
          label: "Anno",
          searchable: true,
          sortable: true,
          width: "30",
        },
        {
          field: "CATEGORIA",
          label: "Cat.",
          searchable: true,
          sortable: true,
          width: "20",
        },
        {
          field: "PROFILO",
          label: "Profilo",
        },
        {
          field: "MANSIONE",
           searchable: true,
          sortable: true,
          label: "Mansione",
        },
        {
          field: "PROFILO_FORMATIVO",
           searchable: true,
          sortable: true,
          label: "Prof. form.",
        },
        {
          field: "MOTIVO_RICHIESTA",
           searchable: true,
          sortable: true,
          label: "Motivo",
        },
        {
          field: "UNITA",
          sortable: true,
          label: "Unità",
        },
        {
          field: "STATO",
           searchable: true,
          sortable: true,
          label: "Stato",
        },
        {
          field: "LAST_DATA",
          sortable: true,
          label: "Ultimo cambio stato",
        },
        {
          field: "ANNOTAZIONI",
          sortable: true,
          label: "Annotazioni",
        },
      ],
    };
  },

  mounted() {
    const loading = this.$buefy.loading.open();
    this.user = JSON.parse(localStorage.getItem("user"));
    this.isAdmin = this.user.role == 1;

    api.mansioni
      .getAll()
      .then((rows) => {
        this.mansioni = rows.data;
        return api.profili.getAll();
      })
      .then((rows) => {
        this.profili = rows.data;
        return api.motiviRichiesta.getAll();
      })
      .then((rows) => {
        this.motiviRichiesta = rows.data;
        return api.fabbisogni.getAll();
      })
      .then((rows) => {
        this.rowData = this.formatRow(rows.data);
        loading.close();
      });
  },
  methods: {
    formatRow(rows) {
      rows.forEach((r) => {
        r.DATA_INS = r.DATA_INS.substring(0, 10);
        r.DATA_ANN = r.DATA_ANN ? r.DATA_ANN.substring(0, 10) : null;
        r.DATA_CONF = r.DATA_CONF ? r.DATA_CONF.substring(0, 10) : null;
        r.DATA_ANALIZZATO = r.DATA_ANALIZZATO
          ? r.DATA_ANALIZZATO.substring(0, 10)
          : null;
        r.STATO = this.stati[r.STATO_ID];
        if (r.DATA_ANALIZZATO) r.LAST_DATA = r.DATA_ANALIZZATO;
        else if (r.DATA_ANN) r.LAST_DATA = r.DATA_ANN;
        else if (r.DATA_CONF) r.LAST_DATA = r.DATA_CONF;
        else if (r.DATA_INS) r.LAST_DATA = r.DATA_INS;
      });
      return rows;
    },
    doDelete() {
      this.$buefy.dialog.confirm({
        message: "Confermi la cancellazione?",
        onConfirm: () => {
          const loading = this.$buefy.loading.open();
          api.fabbisogni
            .delete(this.newItem.ID)
            .then(() => {
              return api.fabbisogni.getAll();
            })
            .then((rows) => {
              this.rowData = this.formatRow(rows.data);
              helpers.ok("Fabbisogno correttamente cancellato");
              this.newItem = emptyVal;
              this.selectedTab = 0;
              loading.close();
            })
            .catch((err) => {
              helpers.error("Errore! " + err);
              loading.close();
            });
        },
      });
    },
    doBack() {
      this.newItem = Object.assign({}, emptyVal);
      this.selectedTab = 0;
      this.selected = null;
    },
    doNew() {
      this.newItem = Object.assign({}, emptyVal);
      this.newItem.DIREZIONE = this.user.direzione;
      this.newItem.STATO = "In corso";
      this.disabled = false;
      this.selectedTab = 1;
    },
    doChangeState(newState) {
      const loading = this.$buefy.loading.open();
      api.fabbisogni
        .changeState(this.newItem.ID, newState)
        .then(() => {
          return api.fabbisogni.getAll();
        })
        .then((rows) => {
          this.rowData = this.formatRow(rows.data);
          helpers.ok("Cambio di stato correttamente eseguito");
          this.newItem = emptyVal;
          this.selectedTab = 0;
          loading.close();
        })
        .catch((err) => {
          helpers.error("Errore! " + err);
          loading.close();
        });
    },
    doConfirmAll(){
      console.warn(this.checkedRows);
      let ids = this.checkedRows.map((x)=>{return x.ID});
      this.$buefy.dialog.confirm({
        message: "Tutti i record selezionati saranno posti in stato 'Confermato'. Procedere?",
        onConfirm: () => {
           const loading = this.$buefy.loading.open();
          let num;
          api.fabbisogni
            .multiConfirm(ids)
            .then((rows) => {
              num = rows.data;
              return api.fabbisogni.getAll();
             })
            .then((rows) => {
              this.rowData = this.formatRow(rows.data);
              helpers.ok(`${num} record confermati`);
              this.newItem = emptyVal;
              this.selectedTab = 0;
              loading.close();
            })
            .catch((err) => {
              helpers.error("Errore! " + err);
              loading.close();
            });
        }
      })  
    },
    doChangeCategory(){
      this.newItem.MANSIONE_ID = 0
    },
    getSelectedRows () {
      if (this.checkedRows.length === 0) {
        return [this.selected]
      } else {
        return this.checkedRows
      }
    },
    doEdit() {
      let val = this.$refs["iCat"].checkHtml5Validity();
      val = val && this.$refs["iProf"].checkHtml5Validity();
      val = val && this.$refs["iProfForm"].checkHtml5Validity();
      val = val && this.$refs["input1"].checkHtml5Validity();
      val = val && this.$refs["iMans"].checkHtml5Validity();

      if (!val) {
        return;
      }
      this.newItem.UTENTE_INS = this.user.username;
      let apiMethod =
        this.newItem.ID != 0 ? api.fabbisogni.update : api.fabbisogni.create;
      const loading = this.$buefy.loading.open();
      apiMethod(this.newItem)
        .then(() => {
          return api.fabbisogni.getAll();
        })
        .then((rows) => {
          this.rowData = this.formatRow(rows.data);
          helpers.ok("Fabbisogno correttamente salvato");
          // Rimane sulla scheda
          //this.newItem = emptyVal;
          //this.selectedTab = 0;
          loading.close();
          if (this.newItem.ID == 0)
            this.doNew();
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
      this.disabled = (this.user.role == 2 && this.newItem.STATO_ID != 0);

    },
  },
};
</script>

<style>
  .hero-body {
    padding-top: 0%!important;
  }
</style>