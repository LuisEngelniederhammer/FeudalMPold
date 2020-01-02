extends Node

enum LOG_TYPE {
    SYSTEM,
    DEBUG,
    INFO,
    WARN,
    ERROR
   };

func _write(_str:String = "", _logType = LOG_TYPE.INFO) -> void:
    var timeStruct = OS.get_time();
    var formatStr = "[%s:%s:%s] %s (%s) - %s";
    var logLevelStr;
    match typeof(_logType):
        LOG_TYPE.SYSTEM:
            logLevelStr = "SYSTEM";
        LOG_TYPE.DEBUG:
            logLevelStr = "DEBUG";
        LOG_TYPE.INFO:
            logLevelStr = "INFO";
        LOG_TYPE.WARN:
            logLevelStr = "WARN";
        LOG_TYPE.ERROR:
            logLevelStr = "ERROR";
        _:
            logLevelStr = "INFO";

    var peerId = "-1";
    if(get_tree().network_peer != null):
        peerId = str(get_tree().get_network_unique_id());

    print(formatStr % [timeStruct.hour, timeStruct.minute, timeStruct.second, logLevelStr, peerId, _str]);
    return;

func debug(_str:String) -> void:
    _write(_str, LOG_TYPE.DEBUG);

func info(_str:String) -> void:
    _write(_str, LOG_TYPE.INFO);

func error(_str:String) -> void:
    _write(_str, LOG_TYPE.ERROR);

func warn(_str:String) -> void:
    _write(_str, LOG_TYPE.WARN);
