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

    return data
}

function removeSingle(number) {
    var data = db.run(`DELETE FROM drones WHERE id=?`, number);

    return data
}

function validateCreate(droneObj) {
    let messages = [];

    console.log(droneObj.type);

    if (!droneObj)
        messages.push('No object is provided');

    if (droneObj.type==undefined)
        messages.push('Type is empty');

    if (!droneObj.name)
        messages.push('Name is empty');

    if (!droneObj.owner)
        messages.push('Owner is empty');

    if (messages.length) {
        let error = new Error(messages.join());
        error.statusCode = 400;

        throw error;
    }
}

function putSingle(droneObj, number) {
    let errMsg = [];

    validateCreate(droneObj);
    
    if (errMsg.length) {
        let error = new Error(errMsg.join());
        error.statusCode = 400;

        throw error;
    }

    var data = db.run(`UPDATE drones
                SET type=?, name=?, owner=?
                WHERE id=?`, [droneObj.type, droneObj.name, droneObj.owner, number]);

    let message = 'Error updating drone';
    if (data.changes)
        message = 'Drone updated successfully';
    
    return {
        data,
        message
    }
}

function patchSingle(droneObj, number) {
    var prevData = db.query(`SELECT * FROM drones WHERE id=?`, number);

    var { type, name, owner } = droneObj; //Drone objs is strings

    if (type === undefined)
        type = prevData[0].type;
    if (name === undefined)
        name = prevData[0].name;
    if (owner === undefined)
        owner = prevData[0].owner;
    
    var data = db.run(`UPDATE drones
                SET type=?, name=?, owner=?
                WHERE id=?`, [type, name, owner, number]);

    return data
}

function create(droneObj) {
    validateCreate(droneObj);
    const { type, name, owner } = droneObj;
    const result = db.run('INSERT INTO drones (type, name, owner) VALUES (@type, @name, @owner)', { type, name, owner });

    let message = 'Error in creating drones';
    if (result.changes) {
        message = 'Drone added successfully';
    }

    return message;
}

module.exports = {
    getMultiple,
    getSingle,
    removeSingle,
    putSingle,
    patchSingle,
    create
}