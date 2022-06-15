const db = require('../services/db');
const config = require('../config');

function getMultiple(page = 1) {
    const offset = (page - 1) * config.listPerPage;
    const data = db.query(`SELECT * FROM drones LIMIT ?,?`, [offset, config.listPerPage]);
    const meta = { page };

    return {
        data,
        meta
    }
}

function getSingle(number) {
    var data = db.query(`SELECT * FROM drones WHERE id=?`,number);

    return {
        data
    }
}

function removeSingle(number) {
    var data = db.run(`DELETE FROM drones WHERE id=?`,number);

    return {
        data
    }
}

function create(droneObj) {
    const { type, name, owner } = droneObj;
    const result = db.run('INSERT INTO drones (type, name, owner) VALUES (@type, @name, @owner)', { type, name, owner });

    let message = 'Error in creating drones';
    if (result.changes) {
        message = 'Drone added successfully';
    }

    return { message };
}

module.exports = {
    getMultiple,
    getSingle,
    removeSingle,
    create
}