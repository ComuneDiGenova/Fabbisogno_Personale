# Fabbisogno del personale 1.0
# Documentazione tecnica

## Contesto e scopo del documento
L’applicazione _Fabbisogno del personale_ è stata sviluppata internamente al Comune di Genova in seguito a una richiesta dell’Ufficio del Personale. Scopo del presente documento è di fornire sufficienti informazioni per la manutenzione dell’applicazione stessa.
Si faccia anche riferimento al file _README.md_ contenuto nei sorgenti.

La presente documentazione si riferisce alla versione dell’applicazione 1.0

## Architettura
L’applicazione è composta da due componenti: 
- un componente server, sviluppato in .NET core. Si tratta di un microservizio, che interagisce col client unicamente via API REST. 
- un componente client, sviluppato in Vue.js. Richiede un ambiente di sviluppo con Node.js, ma una volta compilato risulta un sito statico in httml/javascript

Il backend si appoggia a un database SqlServer.
Si faccia riferimento al file _README.md_ contenuto nei sorgenti per la struttura del repository.

## Installazione dell'ambiente di sviluppo

L'applicazione è basata su framework open source ed è potenzialmente utilizzabile in ambiente Linux, ma è stata testata solo in ambiente Windows 10. In aggiunta, la versione attuale è specializzata per il database Microsoft SqlServer, per il quale sono necessarie licenze. L'applicazione è stata testata sulla versione 2016 di SqlServer.

L'ambiente di sviluppo  necessita:
- Nodejs versione 16+ per il client
- npm versione 8+ per il client
- dotnet core versione 6 per il server  
Si rimanda alla documentazione standard di questi tool per le installazioni relative.

Le altre dipendenze sono contenute nel file package.json (client) e fabbpers.json (server) come di consueto in questo tipo di progetti. 

### Ambiente client
Eseguire 
```
npm run install
```
nella directory Client per scaricare le dipendenze client

### Ambiente server
Eseguire 
```
dotnet restore
```
nella directory Server per installare le dipendenze server

## Configurazione ed esecuzione dei server di sviluppo

### Database
E' necessario avere a disposizione un database sqlserver di versione >= 2009. Anche SqlServerExpress è utilizzabile. Creare un database, configurarlo con una login e eseguire lo script  _Server/DataAccess/Migration/DBInit.sql_.
Utilizzare lo script _CreateUser.sql_ per creare il primo utente amministratore. Gli utenti successivi possono essere creati dall'applicazione.

### Ambiente server
E' fornito un file _appsettings_model.json_: rinominarlo o copiarlo in appsettings.json e completare le informazioni mancanti:
- la connectionstring SqlServer col database sopra configurato
- l'indirizzo del client (per il CORS)
- una secret key per il token JWT. 

Il server può essere fatto partire in debug con un IDE oppure con il comando
```dotnet run```

### Ambiente client
Editare il file _src/config/config.js_ se ritenuto necessario. L'applicazione ine ambiente di sviluppo può funzionare correttamente senza editing.
Il server dell'ambiente di sviluppo può essere fatto partire con il comando
```npm run serve```

## Rilascio in produzione
Per il rilascio in produzione è necessario compilare il client e il server e copiare i file risultanti in una directory web accessibile.

### Compilazione del client
- Editare il file _src/config/config_prod.js_ indicando l'indirizzo su cui si vuole installare il componente server.
- Se lo si ritiene opportuno, modificare la versione dell'applicazione nel file _package.json_
- Esegui il comando "./release.ps1" da una shell Powershell. Questo comando copia il file di configurazione sopra editato come config.js primario e compila il client in una directory _dist_.

### Compilazione del server
Eseguire il comando ```dotnet build --configuration Release``` nella directory _Server_. Questo comando compila il server in una directory _bin/Release/net6.0/publish_.

### Rilascio del client
Copiare il contenuto della directory _dist_ nella directory web accessibile. Si tratta di una applicazione statica, che non richiede un server web particolare.

### Rilascio del server 
Copiare il contenuto della directory _bin/Release/net6.0/publish_ in una directory accessibile al un application server. Il server deve essere configurato per eseguire l'applicazione come un'applicazione .NET core. Si rimanda alla documentazione di IIS (o di altro application server) per la configurazione.