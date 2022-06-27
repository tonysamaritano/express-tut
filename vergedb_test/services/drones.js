// const db = require('../services/db');
const db = require('../server.test.js');
// const config = require('../config');

// enumeration for different errors
// associated numbers are formatted in C type 
// of error format where 0 means that everything 
// works and negative numbers flags that something
// is wrong
const ErrorCodes = {
    CreatedSuccessfully:    0,     // code: 201
    BadRequest:            -1,     // code: 400
    NotFound:              -2,     // code: 404
}

function DataBaseError(message, code) {
    const error = new Error(message);
    error.code = code;
    return error;
}

function FindResponseCode(err) {
    // return code 200 ('OK') as default
    let responseCode = 200;
    switch(err.code){
        default: 
        case ErrorCodes.CreatedSuccessfully:
            responseCode = 201;    
        break;
        case ErrorCodes.BadRequest:
            responseCode = 400;
        break;
        case ErrorCodes.NotFound: 
            responseCode = 404;
        break;
    }
    return responseCode;
}

/*
    @return: a list of all drones with no limit
*/
function getAllDrones() {
    const data = db.query(`SELECT * FROM drones`, []);
    if(!data.length){
        throw DataBaseError(`No drones found in database`, ErrorCodes.NotFound)
    }
    return data
}

function getSingle(number) {
    console.log(db.prepare(`SELECT * FROM drones`).all());
    var data = db.query(`SELECT * FROM drones WHERE id=?`,number);
    if(!data.length){
        throw DataBaseError(`Drone with id ${number} does not exist`, ErrorCodes.NotFound);
    }
    return data
}

function removeSingle(number) {
    var data = db.run(`DELETE FROM drones WHERE id=?`, number);
    if(!data.changes){
        throw DataBaseError(`Drone with id ${number} does not exist`, ErrorCodes.NotFound);
    }
    return data
}

function validateCreate(droneObj) {
    let messages = [];

    if (!droneObj)
        messages.push('No object is provided');

    if (droneObj.type==undefined)
        messages.push('Type is empty');

    if (!droneObj.name)
        messages.push('Name is empty');

    if (!droneObj.owner)
        messages.push('Owner is empty');
    
    if(messages.length){
        throw DataBaseError(messages.join(), ErrorCodes.BadRequest);
    }
    
    return messages;
}

function putSingle(droneObj, number) {
    let message = 'Drone updated successfully';

    // if validation was succesful
    if (!validateCreate(droneObj).length) {
        var data = db.run(`UPDATE drones
        SET type=?, name=?, owner=?
        WHERE id=?`, [droneObj.type, droneObj.name, droneObj.owner, number]);

        // if no changes occured, drone did not update successfully
        // can probably delete, this is just a catch all
        if (!data.changes) {
            message = 'Error updating drone'
            throw DataBaseError(`Drone with id ${number} did not update successfully`, 
                                ErrorCodes.BadRequest);
        }
    }

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

    if(!data.changes) {
        throw DataBaseError(`Drone with id ${number} does not exist`, ErrorCodes.NotFound);
    }
    return data
}

function create(droneObj) {
    let message = 'Error in creating drones';

    if(!validateCreate(droneObj).length) {
        const { type, name, owner } = droneObj;
        const result = db.run('INSERT INTO drones (type, name, owner) VALUES (@type, @name, @owner)',
                             { type, name, owner });    
        if (result.changes) {
            message = 'Drone added successfully';
            throw DataBaseError(`Drone added successfully`, ErrorCodes.CreatedSuccessfully);
        }
    }

    return message;
}

module.exports = {
    FindResponseCode,
    getAllDrones,
    getSingle,
    removeSingle,
    putSingle,
    patchSingle,
    create,
}
