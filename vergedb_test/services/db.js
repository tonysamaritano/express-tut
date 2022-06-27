const sqlite = require('better-sqlite3');
const path = require('path');

let db;

if (process.env.NODE_ENV === 'test') {
    db = new sqlite(':memory:');

    db.prepare(`CREATE TABLE IF NOT EXISTS drones(id INTEGER PRIMARY KEY AUTOINCREMENT,
            type INTEGER NOT NULL,
            name TEXT NOT NULL UNIQUE,
            owner TEXT NOT NULL)`).run();
    db.prepare(`DELETE FROM drones`).run();
    db.prepare(`INSERT INTO drones (type, name, owner) VALUES (?, ?, ?)`).run(0, 'X1-001', 'Verge');
    db.prepare(`INSERT INTO drones (type, name, owner) VALUES (?, ?, ?)`).run(1, 'X1-002', 'Verge');
}
else {
    db = new sqlite(path.resolve('drones.db'), { fileMustExist: true });
}

db.serialize({}, () => {
    db.prepare(`CREATE TABLE IF NOT EXISTS drones(id INTEGER PRIMARY KEY AUTOINCREMENT,
        type INTEGER NOT NULL,
        name TEXT NOT NULL UNIQUE,
        owner TEXT NOT NULL)`).run();
})

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