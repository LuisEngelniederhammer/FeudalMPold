extends Object

enum TYPE{
    AUTHENTICATION,
    KICK,
    TEMP_BAN,
    BAN
}

const TYPE_REASONS = {
    TYPE.AUTHENTICATION: "Authentication failure!",
    TYPE.KICK: "Kicked",
    TYPE.TEMP_BAN: "Temporary banned",
    TYPE.BAN: "Banhammer"
}