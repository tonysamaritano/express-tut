const sqlite = require('better-sqlite3');
const path = require('path');

let db;

if (process.env.NODE_ENV === 'test') {
    db = new sqlite(':memory:');
}
else {
    db = new sqlite(path.resolve('drones.db'), { fileMustExist: true });
}

function query(sql, params) {
    return db.prepare(sql).all(params);
}

function run(sql, params) {
    return db.prepare(sql).run(params);
}

module.exports = {
    query,
    run
}