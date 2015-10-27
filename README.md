# BFSCodeReview
Workitems:
•make the code compile. It does not now. (change bool message by bool info and adjust code.)
•separate the code into methods: LogInfo, LogWarning, LogError. Remove unnecessary bool params and separate responsibilities.
•change implementation to a static, using the constructor should have no parameters (all params fetched from config file).
•constructor should have a logtype Enum (none, info, warning, error). That will be the lowest loggable value.. For example: info will log info, warning, error. Warning will log warning and error. None will force an early return.
•Add using() for sqlconnection and sqlcommand as they use the idisposable interface to release resources, to avoid exhausting the available connections and make sure transactions are closed timely.
•no log to console, no log to file, no log to database: it's a valid configuration. It just won’t do anything. doing this should force an early return.
•it's a log, i don’t want it to throw an exception if no message is shown.
•unit tests.
