# Fabbisogno del personale

Un'applicazione web per la gestione del fabbisogno del personale delle diverse direzioni in un Ente Locale

## Descrizione

L'applicazione "Fabbisogno del personale" è un'applicazione pensata per le specifiche esigenze di raccolta dati con workflow di validazione per i fabbisogni del personale.  
La Direzione del Personale negli Enti Locali ha la necessità di saper programmare con un certo anticipo le assunzioni del personale nelle varie direzioni di cui si compone l'Ente. Lo scopo dell'applicazione è di sostituire i fogli excel che implementavano il processo di raccolta dati in questo contesto e validazione, tramite un'interfaccia web e un processo di validazione da parte dell'Ufficio Personale.
![Screenshot](/screenshot.PNG "Fabbisogno del personale")

## Architettura e struttura del repository
L’applicazione è composta da due componenti: 
- un componente server, sviluppato in .NET core. Si tratta di un microservizio, che interagisce col client unicamente via API REST.   
- un componente client, sviluppato in Vue.js 2. Richiede un ambiente di sviluppo con Node.js, ma una volta compilato risulta un sito statico in httml/javascript

Il backend si appoggia a un database SqlServer.

Questa struttura si riflette nella struttura del repository, in due macrodirectory contenenti la parte server e la parte client.  
La struttura del client è quella di un classico progetto node.js, per il quale si rimanda alla documentazione standard.  
Il componente server è costituito da una solution con un unico progetto. Le directory primarie riflettono la classica struttura dei progetti di API REST:
- _Controller_ contiene i controller, cioè le route delle API
- _Business_ contiene la logica di business e viene chiamata dai controller
- _DataAccess_ è il livello di accesso ai dati. Si è scelto, per tenere il progetto snello e più facilmente manutenibile, di non usare ORM quali Entity Framework ma di scrivere direttamente query al database. 

Non esistono attualmente branch al di fuori del master.

## Prerequisiti e dipendenze (ambiente di sviluppo)

L'applicazione è basata su framework open source ed è potenzialmente utilizzabile in ambiente Linux, ma è stata testata solo in ambiente Windows 10. In aggiunta, la versione attuale è specializzata per il database Microsoft SqlServer, per il quale sono necessarie licenze. L'applicazione è stata testata sulla versione 2016 di SqlServer.

L'ambiente di sviluppo  necessita:
- Nodejs versione 16+ per il client
- npm versione 8+ per il client
- dotnet core versione 6 per il server  

Le altre dipendenze sono contenute nel file package.json (client) e fabbpers.json (server) come di consueto in questo tipo di progetti. Eseguire 
```
npm run install
```
nella directory Client per scaricare le dipendenze client


## Configurazione dell'applicazione

Non sono distribuiti binari precompilati. Per l'installazione è pertanto necessario installare l'ambiente di sviluppo e eseguire le istruzioni qui di seguito.

### Configurazione componente client
Editare il file _Client/src/config/config_prod.js_ indicando l'indirizzo su cui si vuole installare il componente server.

### Configurazione componente servers
Editare il file appsettings.json indicando:
- nell'oggetto _JWT_, l'indirizzo del client e una secret key opportunamente sicura
- nell stringa _ConnectionString_,la connection string del database Sqlserver

### Database
Il database riferito nella connection string deve essere inizializzato mediante gli script contenuti nel file _Server/DataAccess/Migrations/DBInit.sql_  
É quindi necessario creare almeno **un utente** con diritti amministrativi, in modo da poter poi, tramite l'applicazione, creare altri utenti. Questo utente può essere successivamente disattivato, a discrezione degli amministratori. La creazione di un utente avviene tramite lo script _Server/DataAccess/Migrations/CreateUser.sql_, che deve essere opportunamente modificato almeno nella password. 

### Esecuzione in modalità debug
L'applicazione può essere eseguita in ambiente di sviluppo mediante i seguenti comandi.
Per il client, posizionarsi con una shell nella directory _Client_ ed eseguire
```
npm run serve
```
Partirà un server di debug sulla porta locale configurata (di default 8000)

Per il server, posizionarsi con una shell nella directory _Server_ ed eseguire
```
dotnet run
```
o, alternativamente, usare un IDE quale Visual Studio o Visual Studio Code. Partirà un server sulla porta 5000 (di default)

L'applicazione è configurata per funzionare out of the box con le porta sopra citate.

## Installazione in ambiente di produzione

### Scelta dei domini
L'applicazione in produzione necessita di due fully qualified domain (FQD), uno per il client e uno per il server. Identificarli prima delle operazioni di compilazione e installazione.

### Server
Configurare il file _appsetting.json_ come sopra indicato nel paragrafo _Configurazione dell'applicazione_.  Questa operazione può anche essere eseguita successivamente alla compilazione o anche al trasferimento dei file sul server.
Compilare i sorgenti usando il comando
```
dotnet build --release
```
posizionandosi con una shell in _Server_. 
La directory _Server/bin/Release_ contiene i binari ottimizzati. Utilizzarli per un'installazione con IIS o con un altro server in grado di gestire i runtime dotnet. 

### Client
Configurare il fully qualified domain del server in _Client/src/config/config_prod.js_.  
Posizionarsi quindi con una powershell nella directory _Client_ ed eseguire il comando:
```
./release.ps1
```
che si occupa di gestire la configurazione e compilare. Verrà generata la directory _Client/dist_. In alternativa, si può configurare il file _Client/src/config/config.js_ ed eseguire il consueto
```
npm run build
```
I file generati sono un sito statico: è sufficiente configurare un qualsivoglia web server (IIS, Apache, nginx...) per servirli utilizzando il FQD individuato per il client.

## Considerazioni sulla sicurezza
L'applicazione usa le seguenti contromisure per gli attacchi più comuni:
- le password sono salvate nel database con SHA1. Si è scelto questo algoritmo per retrocompatibilità con le versioni più vecchie di SqlServer, ma è possibile aggiornare (si veda sotto "Uso di altri database")
- l'autenticazione avviene mediante il meccanismo dei token JWT. Questo sistema previene gli attacchi CSRF.
- l'utilizzo del framework web vue.js nella sua versione 2 (sistema nella top 5 dei framework web più utilizzati) permette sufficiente garanzia contro gli attacchi XSS
- le query al database utilizzano **esclusivamente** parametri e non composizione di stringhe, prevenendo attacchi di SqlInjection

## Utilizzo di altri database backend
L'applicazione, nella sua versione attuale, è pensata per funzionare esclusivamente su SqlServer. Tuttavia, le query e il DDL che si trovano nella sezione _Server/DataAccess_ sono quasi esclusivamente in SQL standard e non dovrebbero richiedere particolari modifiche nel caso dell'utilizzo di un altro backend. Si presti solo attenzione alla parte crittografica relativa all'hash delle password, per le quali ogni db engine ha le sue funzionalità.
Sarà inoltre necessario usare le librerie di dotnet specifiche per ogni motore.  
Future versioni di _Fabbisogno del personale_ potrebbero includere il supporto multidatabase.

## Supporto
Il progetto _Fabbisogno del personale_ è sviluppato dal Comune di Genova. Per ulteriori informazioni e supporto si apra un ticket su https://comunegenova.atlassian.net/servicedesk/customer/portal/3 scegliendo come applicativo "Altro" e specificando le necessità della descrizione.

## Stato del progetto
Il progetto è stato rilasciato ed è in uso nel Comune di Genova dal 2021. Non son state richieste ulteriori funzionalità e non sono attualmente pianificati aggiornamenti futuri.

### Documentazione
Nella directory _Doc_ è compreso un file  _Documentazione utente_ contenente la documentazione pensata per l'utente finale. La versione attuale non prevede un help online nell'applicazione stessa.

### Segnalazioni problematiche si sicurezza
Eventuali segnalazioni relative alla sicurezza dell'applicazione si apra un ticket all'indirizzo sopra, oppure si mandi una mail a gestionesistemi@comune.genova.it  e **NON** si usi l'issue tracker pubblico.

### Segnalazioni anomalie e pull request
Per le segnalazioni di anomalie non relative alla sicurezza e le  pull request si richiede l'utilizzo dell'issue tracker di Github invece della mail.

## Autori

Fabbisogno del Personale è stato sviluppato da Luca Ventimiglia ( lventimiglia@comune.genova.it ) nell'ambito del Comune di Genova.

## Storico versioni

* 1.0
    * Primo rilascio di versione stabile

## Licenza

Questo progetto è rilasciato con la European Union Public Licence 1.2. Si veda il file LICENSE.md per dettagli.



