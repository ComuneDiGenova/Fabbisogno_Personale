// SPDX-License-Identifier: EUPL-1.2
import { ToastProgrammatic as Toast } from 'buefy'
import api from "./api/api.js";

let ok = (message) => {
    Toast.open({message,type:'is-success', position:'is-top-right'});
}

let error = (message) => {
    Toast.open({message,type:'is-danger', position:'is-top-right',duration:3000});
}

let buildFilter = (filter) => {
    let f = [];
    let fr = [];
    if (filter.Title) {
        f.push({
            "Param": "movies.title",
            "Value": "%"+filter.Title+"%"
        })
    }
    if (filter.OriginalTitle) {
        f.push({
            "Param": "movies.original_title",
            "Value": "%"+filter.OriginalTitle+"%"
        })
    }
    if (filter.Id) {
        f.push({
            "Param": "shows.id",
            "Value": filter.Id.toString()
        })
    }
    if (filter.City_id) {
        f.push({
            "Param": "shows.city_id",
            "Value": filter.City_id.toString()
        })
    }
    if (filter.Mode_id) {
        f.push({
            "Param": "shows.mode_id",
            "Value": filter.Mode_id.toString()
        })
    }
    if (filter.Cinema_id) {
        f.push({
            "Param": "shows.cinema_id",
            "Value": filter.Cinema_id.toString()
        })
    }
    if (filter.Language_id) {
        f.push({
            "Param": "shows.language_id",
            "Value": filter.Language_id.toString()
        })
    }
    if (filter.Year) {
        f.push({
            "Param": "movies.year",
            "Value": filter.Year.toString()
        })
    }
    if (filter.DateStart) {
        f.push({
            "Param": "shows.dateStart",
            "Value": buildIsoDate(filter.DateStart)
        })
    }
    if (filter.DateEnd) {
        f.push({
            "Param": "shows.dateEnd",
            "Value": buildIsoDate(filter.DateEnd)
        })
    }
    if (filter.Friends) {
        fr = filter.Friends.map((x)=>{return x.Id});
    }
    return {Clauses:f,Friends:fr}
}

let flattenVisions = (visions,lovs) => {
    let fullMode = (vision, lovs, isSimple) => {
        let mode = lookupLOV(lovs,"modes",vision.Ext_show.Mode_id,"Mode");
        let city = lookupLOV(lovs,"cities",vision.Ext_show.City_id,"City");
        let cinema_name = lookupLOV(lovs,"cinemas",vision.Ext_show.Cinema_id,"Name");
        let cinema_room = lookupLOV(lovs,"cinemas",vision.Ext_show.Cinema_id,"Room");
        let str = "";
        if (isSimple) {
            if (cinema_name) {
                str = `${cinema_name}${cinema_room?' '+cinema_room:''}` 
            } else
                str = mode;
            str += ", " + city + " - ";
            }
            
        else {
            if (mode=="Cinema") {
                str = "al cinema " + `${cinema_name} ${cinema_room?cinema_room:''}` 
            } else if (mode=="Streaming") {
                str = "in streaming" +  `${cinema_name?" su " + cinema_name:''}`;
            } else 
                str = "in " + mode;
            str += ", a " + city + " in ";
        }
           
        str += lookupLOV(lovs,"languages",vision.Ext_show.Language_id,"Language");
        return str; 

    };
    let fullTitle = (vision) => {
        let or_title = vision.Ext_show.Movie_original_title == vision.Ext_show.Movie_title?"id.":vision.Ext_show.Movie_original_title;
        let title = `${vision.Ext_show.Movie_title} (${or_title},${vision.Ext_show.Movie_year})`;
        if (vision.Directors.length == 0) return title;
        title = `${title} di ${vision.Directors[0].Name}`;
        if (vision.Directors.length>1) 
            title += " etc.";
        return title;
    }
    let out = [];
    visions.forEach(vision=>{
        out.push({
            ...vision,
            Id: vision.Ext_show.Id,
            Full_title : fullTitle(vision),
            Date_seen: buildDate(vision.Ext_show.Date_seen),
            Dicks: vision.Ext_show.Dicks,
            Quality:vision.Ext_show.Quality,
            Comment: vision.Ext_show.Comment,
            Full_mode: fullMode(vision, lovs, true),
            Full_mode_desc: fullMode(vision, lovs, false),
        });
    })
    return out;
}

let lookupLOV = (lovs,type,val,column) => {
    if (!val)
     return "";
    let lov = lovs[type];
    if (!lov)
        return "-"
    let item = lov.filter((x)=>{return x.Id==val});
    if (item.length>0) return item[0][column]; else return "-";
}

let adjLov= (lovs,lovType,id,column,store) => {
    let c = lookupLOV(lovs,lovType,id,column);
    if (c=="-") {
      let newC = store.filter(x=>{return x.Id == id});
      lovs[lovType].push(newC[0])
    }
}

let buildDate = (secs) => {
    let microFormat = (x) => {
        if (x<10)
            return "0"+x;
        else 
            return x.toString()
    }
    let d = new Date(secs*1000);
    return `${d.getFullYear()}-${microFormat(d.getMonth()+1)}-${microFormat(d.getDate())}`;
}

let buildIsoDate = (date) => {
    var tzoffset = (new Date()).getTimezoneOffset() * 60000; 
    return (new Date(date - tzoffset)).toISOString().substring(0,10)
}

let getLovs = () => {
    let data = {};
    return new Promise((resolve, reject) => {
        api.mode.getAll()
        .then( (modes)=> {
            data.modes = modes.data;
            return api.city.getAll()
        })
        .then( (cities)=> {
            data.cities = cities.data;
            return api.friend.getAll()
        })
        .then( (friends)=> {
            data.friends = friends.data;
            return api.language.getAll()
        })
        .then( (languages)=> {
            data.languages = languages.data;
            return api.cinema.getAll()
        })
        .then( (cinemas)=> {
            data.cinemas = cinemas.data;
            resolve(data);
        })
        .catch ((err)=>{
            reject(err);
        })
    })
}

export default {
    ok,
    error,
    flattenVisions,
    lookupLOV,
    adjLov,
    buildDate,
    buildFilter,
    buildIsoDate,
    getLovs
}