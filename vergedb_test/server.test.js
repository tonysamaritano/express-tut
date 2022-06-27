const { app } = require('./server.js');
const {getType} = require('jest-get-type');
const sqlite3 = require('better-sqlite3');
const request = require('supertest');
const db = new sqlite3(':memory:');
const express = require('express');

let seedDb;
beforeAll(() => {
    process.env.NODE_ENV = 'test';
    initializeDroneDatabase();
    console.log("reached before all");
});

afterAll(() => {
    deinitializeDroneDatabase();
    console.log("reached after all");
})

// beforeEach(() => {
//     return 
// })

function initializeDroneDatabase(){
    seedDb = db => {
        db.prepare(`CREATE TABLE IF NOT EXISTS drones(id INTEGER PRIMARY KEY AUTOINCREMENT,
            type INTEGER NOT NULL,
            name TEXT NOT NULL UNIQUE,
            owner TEXT NOT NULL)`).run();
        db.prepare(`DELETE FROM drones`).run();
        db.prepare(`INSERT INTO drones (type, name, owner) VALUES (?, ?, ?)`).run(0, 'X1-001', 'Verge');
        db.prepare(`INSERT INTO drones (type, name, owner) VALUES (?, ?, ?)`).run(1, 'X1-002', 'Verge');
        console.log("database initialized");
        console.log(db.prepare(`SELECT * FROM drones`).all());
        // const stmt = db.prepare('INSERT INTO drones (type, name, owner) VALUES (?, ?, ?)');
        // stmt.run(0, 'X1-001', 'Verge');
        // stmt.run(1, 'X1-002', 'Verge');
        // stmt.run(0, 'X1-003', 'Image');
        // stmt.run(1, 'X1-004', 'Image');
        // stmt.run(0, 'X1-005', 'Verge');
    
        // stmt.finalize();
    }
}

function deinitializeDroneDatabase(){
    seedDb = db => {
        db.prepare(`DELETE FROM drones`);
    console.log("database deinitialized");
    }
}

function query(sql, params) {
    return db.prepare(sql).all(params);
}

function run(sql, params) {
    console.log('running run in db');
    return db.prepare(sql).run(params);
}

/* GET Single id test */
describe('get drone by single id test', () => {
    test('get first entry', () => {
        console.log("1st id promises test");
        // seedDb(db);
        const response = [
            { "id": 1, type: 0, name: 'X1-001', owner: 'Verge' }
        ]

        return request(app).get("/drones/1")
            .then(res => {
                expect(res.status).toBe(200);
                console.log(res.body);
                expect(res.body).toEqual(response);
            });
    });
});

/* GET Single id test */
describe('get drone by single id test intentional fail', () => {
    test('get first entry', () => {
        console.log("1st id promises test");
        // seedDb(db);
        const response = [
            { "id": 1, type: 0, name: 'X1-002', owner: 'Verge' }
        ]

        return request(app).get("/drones/1")
            .then(res => {
                expect(res.status).toBe(200);
                console.log(res.body);
                expect(res.body).toEqual(response);
            });
    });
});

/* POST test */
describe('add single drone via post', () => {
    test('add drone', () => {
        // seedDb(db);
        return request(app)
            .post('/')
            .send({ type: 1, name: 'X1-006', owner: 'Verge' })
            .then(res => {
                expect(res.status).toBe(201);
            });
    });
});

// /* PUT test */
// test('update drone', () => {
//     db.serialize(async () => {
//         seedDb(db);
//         await request(server)
//             .put('/6')
//             .send({ type: 1, name: 'X1-006', owner: 'Tony' });
//         const res = await request(server).get('/6');
//         const response = [
//             { type: 1, name: 'X1-006', owner: 'Tony' }
//         ]
//         expect(res.status).toBe(200);
//         expect(res.body).toEqual(response);
//     })
// });

// /* PATCH test */
// test('patch drone', () => {
//     db.serialize(async () => {
//         seedDb(db);
//         await request(server)
//             .patch('/6')
//             .send({ name: 'Patch Name' });
//         const res = await request(server).get('/6');
//         const response = [
//             { type: 1, name: 'Patch Name', owner: 'Tony' }
//         ]
//         expect(res.status).toBe(200);
//         expect(res.body).toEqual(response);
//     })
// });

// /* DELETE test */
// test('delete drone', () => {
//     db.serialize(async () => {
//         seedDb(db);
//         const res = await request(server).delete('/1');
//         const response = [];
//         expect(res.status).toBe(200);
//         expect(res.body).toEqual(response);
//     })
// });

module.exports = {
    // db,
    query,
    run
}
