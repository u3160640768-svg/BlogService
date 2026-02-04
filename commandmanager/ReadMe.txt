TESTO DEL PROBLEMA
Sei incaricato di progettare e implementare un sistema di Gestione del Flusso di Dati per un'applicazione critica, aderendo ai principi dell'architettura Clean Code (struttura a strati).

L'obiettivo è separare chiaramente la logica di business (nel layer Application) dai meccanismi di persistenza (nel layer Infrastructure).

Dominio
Devi definire le entità di base che rappresentano i dati che transitano nel sistema, le quali saranno immuni ai cambiamenti nelle tecnologie di persistenza.

Entità: Crea la classe base astratta Command e le due classi derivate:

LogEntry: Per i dati che devono essere elaborati in ordine cronologico.

UserAction: Per i comandi utente che devono supportare la funzionalità di "undo".

Contratto: Queste classi devono definire solo la struttura del dato e un metodo per ottenere i dettagli (GetDetails()), ma non devono contenere alcuna logica di Stack, Queue o di serializzazione/file I/O.

Application
Questo strato contiene la logica del sistema di gestione dei dati (i gestori di Stack e Queue) e definisce i contratti per l'interazione con il mondo esterno (I/O).

Servizio: Implementa la classe CommandProcessor che deve:

Mantenere internamente uno Queue<LogEntry> e uno Stack<UserAction>.

Avere metodi per aggiungere i comandi/log (AddData), processare i log (ProcessAllLogs - FIFO), e annullare le azioni (UndoLastAction - LIFO).

Contratto (DIP): Definire l'interfaccia IDataStore (nel layer Application) che specifica i metodi necessari per salvare e caricare lo stato del sistema. Il CommandProcessor deve dipendere da questa interfaccia e non da un'implementazione concreta (es. file JSON)

Infrastruttura/Persistenza
Questo strato si occuperà dei dettagli tecnici per la persistenza dello stato, in modo che il layer Application rimanga pulito.

Implementazione: Implementare la classe JsonDataStore che implementa l'interfaccia IDataStore. Questa classe deve:

Utilizzare la serializzazione JSON per convertire gli oggetti Command in una stringa e viceversa.

Gestire la lettura e scrittura su un file fisico (es. commands_data.json).

Vincolo: Questa è l'unica parte del codice che deve contenere dipendenze dalle librerie di serializzazione (es. System.Text.Json).
_____________________________________________________________

Scenario di Test (Utilizzo)
Simulare il seguente flusso utilizzando le classi implementate:

Ricezione: Aggiungere un mix di LogEntry e UserAction al CommandProcessor.

Salvataggio: Chiamare un metodo per salvare lo stato utilizzando la dipendenza IDataStore.

Caricamento: Creare una nuova istanza del CommandProcessor e caricare lo stato salvato (dimostrando che la serializzazione ha funzionato).

Esecuzione: Eseguire ProcessAllLogs (FIFO) e UndoLastAction (LIFO) sulla nuova istanza.