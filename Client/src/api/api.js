// SPDX-License-Identifier: EUPL-1.2
let config = require("../config/config.js")
import axios from "axios";
//import helpers from "../helpers.js";

/*axios.interceptors.response.use(function (response) {
  return response;
}, function (error) {
  helpers.error("Ooops: " + error.toString());
  return Promise.reject(error);
})*/

//let baseUrl = process.env.VUE_APP_API_BASE_URL;
//if (!baseUrl) 
let baseUrl = config.api;


let setUpConfig = function (method, suffix) {
  /*config = {
    headers:{},
    method,
    url:baseUrl+suffix
  }*/

  let user = localStorage.getItem('user')
  let token = user ? (JSON.parse(user)).token : null;
  let config = {}

  if (token) {
    config = {
      headers: { 'Authorization': `Bearer ${token}` },
      method,
      url: baseUrl + suffix
    }
  }

  else {
    console.error("No token saved!");
  }

  return config;
}

let mansioni = {
  getAll: () => {
    let axiosConfig = setUpConfig("get", "mansioni/");
    return axios(axiosConfig);
  },

  create: (data) => {
    let axiosConfig = setUpConfig("post", "mansioni/");
    axiosConfig.headers["Content-Type"] = "application/json";
    axiosConfig.data = { NOME: data.NOME, ID: 0 };
    return axios(axiosConfig);
  },

  update: (data) => {
    let axiosConfig = setUpConfig("put", "mansioni/");
    axiosConfig.headers["Content-Type"] = "application/json";
    axiosConfig.data = { NOME: data.NOME, ID: data.ID };
    return axios(axiosConfig);
  },

  delete: function (id) {
    let axiosConfig = setUpConfig("delete", "mansioni/" + id);
    return axios(axiosConfig);
  }
}

let utenti = {
  getAll: () => {
    let axiosConfig = setUpConfig("get", "utenti/");
    return axios(axiosConfig);
  },

  create: (data) => {
    let axiosConfig = setUpConfig("post", "utenti/");
    axiosConfig.headers["Content-Type"] = "application/json";
    data.ATTIVO=data.ATTIVO_B?1:0;
    axiosConfig.data = data;
    return axios(axiosConfig);
  },

  update: (data) => {
    let axiosConfig = setUpConfig("put", "utenti/");
    axiosConfig.headers["Content-Type"] = "application/json";
    data.ATTIVO=data.ATTIVO_B?1:0;
    axiosConfig.data = data;
    return axios(axiosConfig);
  }
}

let profili = {
  getAll: () => {
    let axiosConfig = setUpConfig("get", "profili/");
    return axios(axiosConfig);
  },

  create: (data) => {
    let axiosConfig = setUpConfig("post", "profili/");
    axiosConfig.headers["Content-Type"] = "application/json";
    axiosConfig.data = { NOME: data.NOME, CATEGORIA: data.CATEGORIA, ID: 0 };
    return axios(axiosConfig);
  },

  update: (data) => {
    let axiosConfig = setUpConfig("put", "profili/");
    axiosConfig.headers["Content-Type"] = "application/json";
    axiosConfig.data = data;
    return axios(axiosConfig);
  },

  delete: function (id) {
    let axiosConfig = setUpConfig("delete", "profili/" + id);
    return axios(axiosConfig);
  }
}

let motiviRichiesta = {
  getAll: () => {
    let axiosConfig = setUpConfig("get", "motivi/");
    return axios(axiosConfig);
  },

  create: (data) => {
    let axiosConfig = setUpConfig("post", "motivi/");
    axiosConfig.headers["Content-Type"] = "application/json";
    axiosConfig.data = { NOME: data.NOME, ID: 0 };
    return axios(axiosConfig);
  },

  update: (data) => {
    let axiosConfig = setUpConfig("put", "motivi/");
    axiosConfig.headers["Content-Type"] = "application/json";
    axiosConfig.data = { NOME: data.NOME, ID: data.ID };
    return axios(axiosConfig);
  },

  delete: function (id) {
    let axiosConfig = setUpConfig("delete", "motivi/" + id);
    return axios(axiosConfig);
  }
}


let fabbisogni = {
  getAll: () => {
    let axiosConfig = setUpConfig("get", "fabbisogni/");
    return axios(axiosConfig);
  },
  create: (fabb) => {
    let axiosConfig = setUpConfig("post", "fabbisogni/");
    axiosConfig.headers["Content-Type"] = "application/json";
    fabb.UNITA= parseInt(fabb.UNITA);
    fabb.ANNO= parseInt(fabb.ANNO);
    axiosConfig.data = fabb;
    return axios(axiosConfig);
  },
  update: (fabb) => {
    let axiosConfig = setUpConfig("put", "fabbisogni");
    axiosConfig.headers["Content-Type"] = "application/json";
    fabb.ANNO= parseInt(fabb.ANNO);
    fabb.UNITA= parseInt(fabb.UNITA);
    axiosConfig.data = fabb;
    return axios(axiosConfig);
  },
  changeState: (id, stato) => {
    let axiosConfig = setUpConfig("put", "fabbisogni/stato");
    axiosConfig.headers["Content-Type"] = "application/json";
    axiosConfig.data = {Id: id, NuovoStato: stato};
    return axios(axiosConfig);
  },
  multiConfirm: (ids) => {
    let axiosConfig = setUpConfig("put", "fabbisogni/multiconferma");
    axiosConfig.headers["Content-Type"] = "application/json";
    axiosConfig.data = {Ids: ids};
    return axios(axiosConfig);
  },
  delete: function (id) {
    let axiosConfig = setUpConfig("delete", "fabbisogni/" + id);
    return axios(axiosConfig);
  }
}


let lovs = () => {
  let axiosConfig = setUpConfig("get", "lovs");
  return axios(axiosConfig);
}

let login = function (Username, Password) {
  let data = { Username, Password };
  let axiosConfig = {
    url: baseUrl + "login",
    method: 'post',
    headers: { "Content-Type": "application/json" },
    data: data
  }
  return axios(axiosConfig);

}


let api = {
  mansioni,
  fabbisogni,
  motiviRichiesta,
  profili,
  utenti,
  lovs,
  login,
}

export default api;
