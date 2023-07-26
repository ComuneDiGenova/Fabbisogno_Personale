// SPDX-License-Identifier: EUPL-1.2
import Vue from 'vue'
import VueRouter from 'vue-router'
import Mansioni from "@/views/Mansioni"
import Profili from "@/views/Profili"
import Utenti from "@/views/Utenti"
import Fabbisogni from "@/views/Fabbisogni"
import MotiviRichiesta from "@/views/MotiviRichiesta"
import Login from "@/views/Login"



Vue.use(VueRouter)

  const routes = [
    {
      path: '/mansioni',
      name: 'Mansioni',
      component: Mansioni
    },
    {
      path: '/profili',
      name: 'Profili',
      component: Profili
    },
    {
      path: '/motivi',
      name: 'MotiviRichiesta',
      component: MotiviRichiesta
    },
    {
      path: '/utenti',
      name: 'Utenti',
      component: Utenti
    },
    {
      path: '/',
      name: 'Fabbisogni',
      component: Fabbisogni
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
 
]

const router = new VueRouter({
  routes
})

router.beforeEach((to,_from, next) =>{
  let authRequired = to.path!="/login";
  
  let loggedIn = localStorage.getItem("user");
  if (authRequired && !loggedIn) {
    return next('/login')
  }
  next();
})

export default router
