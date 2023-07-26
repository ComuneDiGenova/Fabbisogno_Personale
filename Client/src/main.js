// SPDX-License-Identifier: EUPL-1.2
import Vue from 'vue'
import Buefy from 'buefy'
import 'buefy/dist/buefy.css'
import App from './App.vue'
import router from './router'
import Vuex from 'vuex'

Vue.config.productionTip = false
Vue.use(Buefy);
Vue.use(Vuex);
const store = new Vuex.Store({
  state: {
    user:{}
  },
  mutations : {
  
  },
  actions: {
   
  }
})

new Vue({router, store:store,
  render: h => h(App),
}).$mount('#app')

