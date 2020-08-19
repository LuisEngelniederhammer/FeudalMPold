extends Node

const FMP_VERSION = "0.3.1"
const FMP_TITLE = "FeudalMP"

const PATH_UI:String = "res://assets/ui";
const PATH_SCENES:String = "res://assets/scenes";
const PATH_SRC:String = "res://src";

const NODE_PATH_BASE:String = "/root/FeudalMP"
const NODE_PATH_NETWORK_CONTROLLER:String = NODE_PATH_BASE + "/NetworkController"
const NODE_PATH_SCENE:String = NODE_PATH_BASE + "/Scene"

const FMP_NETWORK_CONTROLLER_NODE = "FMP_NetworkController";
const FMP_SCENE_NODE = "FMP_Scene";

const ERROR_CODES = {
	1:{"code": "FAILED", "message": "Generic error."},
	2:{"code": "ERR_UNAVAILABLE", "message": "Unavailable error."},
	3:{"code": "ERR_UNCONFIGURED", "message": "Unconfigured error."},
	4:{"code": "ERR_UNAUTHORIZED", "message": "Unauthorized error."},
	5:{"code": "ERR_PARAMETER_RANGE_ERROR", "message": "Parameter range error."},
	6:{"code": "ERR_OUT_OF_MEMORY", "message": "Out of memory (OOM) error."},
	7:{"code": "ERR_FILE_NOT_FOUND", "message": "File: Not found error."},
	8:{"code": "ERR_FILE_BAD_DRIVE", "message": "File: Bad drive error."},
	9:{"code": "ERR_FILE_BAD_PATH", "message": "File: Bad path error."},
	10:{"code": "ERR_FILE_NO_PERMISSION", "message": "File: No permission error."},
	11:{"code": "ERR_FILE_ALREADY_IN_USE", "message": "File: Already in use error."},
	12:{"code": "ERR_FILE_CANT_OPEN", "message": "File: Can't open error."},
	13:{"code": "ERR_FILE_CANT_WRITE", "message": "File: Can't write error."},
	14:{"code": "ERR_FILE_CANT_READ", "message": "File: Can't read error."},
	15:{"code": "ERR_FILE_UNRECOGNIZED", "message": "File: Unrecognized error."},
	16:{"code": "ERR_FILE_CORRUPT", "message": "File: Corrupt error."},
	17:{"code": "ERR_FILE_MISSING_DEPENDENCIES", "message": "File: Missing dependencies error."},
	18:{"code": "ERR_FILE_EOF", "message": "File: End of file (EOF) error."},
	19:{"code": "ERR_CANT_OPEN", "message": "Can't open error."},
	20:{"code": "ERR_CANT_CREATE", "message": "Can't create error."},
	21:{"code": "ERR_QUERY_FAILED", "message": "Query failed error."},
	22:{"code": "ERR_ALREADY_IN_USE", "message": "Already in use error."},
	23:{"code": "ERR_LOCKED", "message": "Locked error."},
	24:{"code": "ERR_TIMEOUT", "message": "Timeout error."},
	25:{"code": "ERR_CANT_CONNECT", "message": "Can't connect error."},
	26:{"code": "ERR_CANT_RESOLVE", "message": "Can't resolve error."},
	27:{"code": "ERR_CONNECTION_ERROR", "message": "Connection error."},
	28:{"code": "ERR_CANT_ACQUIRE_RESOURCE", "message": "Can't acquire resource error."},
	29:{"code": "ERR_CANT_FORK", "message": "Can't fork process error."},
	30:{"code": "ERR_INVALID_DATA", "message": "Invalid data error."},
	31:{"code": "ERR_INVALID_PARAMETER", "message": "Invalid parameter error."},
	32:{"code": "ERR_ALREADY_EXISTS", "message": "Already exists error."},
	33:{"code": "ERR_DOES_NOT_EXIST", "message": "Does not exist error."},
	34:{"code": "ERR_DATABASE_CANT_READ", "message": "Database: Read error."},
	35:{"code": "ERR_DATABASE_CANT_WRITE", "message": "Database: Write error."},
	36:{"code": "ERR_COMPILATION_FAILED", "message": "Compilation failed error."},
	37:{"code": "ERR_METHOD_NOT_FOUND", "message": "Method not found error."},
	38:{"code": "ERR_LINK_FAILED", "message": "Linking failed error."},
	39:{"code": "ERR_SCRIPT_FAILED", "message": "Script failed error."},
	40:{"code": "ERR_CYCLIC_LINK", "message": "Cycling link (import cycle) error."},
	41:{"code": "ERR_INVALID_DECLARATION", "message": "Invalid declaration error."},
	42:{"code": "ERR_DUPLICATE_SYMBOL", "message": "Duplicate symbol error."},
	43:{"code": "ERR_PARSE_ERROR", "message": "Parse error."},
	44:{"code": "ERR_BUSY", "message": "Busy error."},
	45:{"code": "ERR_SKIP", "message": "Skip error."},
	46:{"code": "ERR_HELP", "message": "Help error."},
	47:{"code": "ERR_BUG", "message": "Bug error."},
	48:{"code": "ERR_PRINTER_ON_FIRE", "message": "Printer on fire error. (This is an easter egg, no engine methods return this error code.)"}
}
