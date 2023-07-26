<-- SPDX-License-Identifier: EUPL-1.2 -->
<template>
  <section class="hero">
    <div class="hero-body">
      <div class="container">
        <h1 class="title">
          Profili
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
            <div style="cursor:pointer">
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
                  <b-button
                    v-show="newItem.ID!=0"
                    type="is-danger"
                    @click="doDelete"
                  >
                    Cancella il record
                  </b-button>
                </div>
              </section>
              <section>
                <span
                  v-if="newItem.Id"
                  class="subtitle"
                >Id: {{ newItem.ID }}</span>
                <b-field label="Categoria">
                  <b-select v-model="newItem.CATEGORIA">
                    <option
                      v-for="cat in categorie"
                      :key="cat"
                      :value="cat"
                    >
                      {{ cat }}
                    </option>
                  </b-select>
                </b-field>
                <b-field label="Profilo">
                  <b-input
                    ref="input1"
                    v-model="newItem.NOME"
                    required
                    validation-message="Scrivi qualcosa!"
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

const emptyVal = {ID: 0, NOME:"", CATEGORIA:"B"};

export default {
  name: 'Profili',
  data() {
        return {
            selectedRow: null,
            newItem: emptyVal,
            categorie: ["B", "C", "D", "Dir"],
            rowData: [],
            selected: null,
            selectedTab: 0,
            columns: [
                {
                    field: 'ID',
                    label: 'id',
                    width: '40',
                    numeric: true
                },
                {
                    field: 'CATEGORIA',
                    label: 'Categoria',searchable: true,
                    sortable: true,
                    width: '40'
                },
                {
                    field: 'NOME',
                    label: 'Profilo',searchable: true,
                    sortable: true
                }
            ]
        }
    },
    mounted() {
      const loading = this.$buefy.loading.open();
        api.profili.getAll()
        .then ((rows)=>{
          this.rowData = rows.data;
          loading.close();
        })
    },
  methods: {
    doDelete() {
      this.$buefy.dialog.confirm({
        message: 'Confermi la cancellazione?',
        onConfirm: () => {
          const loading = this.$buefy.loading.open();
          api.profili.delete(this.newItem.ID) 
            .then (()=>{
              return api.profili.getAll()
            })
            .then ((rows)=>{
              this.rowData = rows.data;
              helpers.ok('Profilo correttamente cancellato');
              this.newItem = emptyVal;
              this.selectedTab = 0;
              loading.close();
            })
            .catch ((err)=> {
              helpers.error("Errore! " + err)
              loading.close();
            })
        }
    })
    },
    doNew() {
      this.newItem = Object.assign({},emptyVal);
      this.selectedTab = 1;
    },
    doBack() {
      this.newItem = emptyVal;
      this.selectedTab = 0;
      this.selected = null;
    },
    doEdit() {
      if(!this.$refs["input1"].checkHtml5Validity()) {
        return;
      }
      let apiMethod = this.newItem.ID!=0?api.profili.update:api.profili.create;
      const loading = this.$buefy.loading.open();
      apiMethod(this.newItem)
      .then (()=>{
        return api.profili.getAll()
      })
      .then ((rows)=>{
          this.rowData = rows.data;
          helpers.ok('Profilo correttamente salvato');
          this.newItem = emptyVal;
          this.selectedTab = 0;
          loading.close();
      })
      .catch ((err)=> {
        helpers.error("Errore! " + err)
        loading.close();
      })
      
    },
    rowClicked(row) {
      if (row.ID==this.newItem.ID) {
         this.selected = null;
         this.newItem = emptyVal;
       }else {
         this.selectedTab=1;
       }
        
    },
    select(row) {
       this.newItem = row;
    }    
  }
}
</script>

<style>
 .buttons {
   padding-top: 10px;
 }
</style>