# BFSCodeReview

NOTE: had i been involved in earlier stages, I would have recommended against using  time/effort in coding the logger, i would have evaluated Log4Net as a logging tool which is an already tested/working component.

Workitems:
• make the code compile. It does not now. (change bool message by bool info and adjust code)

• separate the code into methods: LogMessage, LogWarning, LogError. Remove unnecessary bool params and separate responsibilities.

• Change JobLogger to do singleton (all params fetched from config file).

• Add using() for sqlconnection and sqlcommand as they use the idisposable interface to release resources, to avoid exhausting the available connections and make sure transactions are closed timely.

• Add the connection to the sqlcommand for it to run properly.

• if are inproperly nested (tests dont pass).

• no log to console, no log to file, no log to database: it's a valid configuration. It just won’t do anything. doing this should force an early return.

• Unit tests (currently 100% pass 100% code coverage, but for some of them the ASSERT portion is invalid as of now).
