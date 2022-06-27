const { app } = require('./server.js');
const {getType} = require('jest-get-type');
const sqlite3 = require('better-sqlite3');
const request = require('supertest');
const db = new sqlite3(':memory:');

beforeAll(() => {
    process.env.NODE_ENV = 'test';
});

afterAll(() => {
    deinitializeDroneDatabase();
})

function deinitializeDroneDatabase(){
    seedDb = db => {
        db.prepare(`DELETE FROM drones`);
    }
}

/* GET Single id test */
describe('get drones by single id test', () => {
    test('get first entry', () => {
        const response = [
            { "id": 1, type: 0, name: 'X1-001', owner: 'Verge' }
        ]

        return request(app).get("/drones/1")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).toEqual(response);
            });
    });

    test('get second entry', () => {
        const response = [
            { "id": 2, type: 1, name: 'X1-002', owner: 'Verge' }
        ]

        return request(app).get("/drones/2")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).toEqual(response);
            });
    });

    test('get single name fail check', () => {
        const response = [
            { "id": 2, type: 1, name: 'X1-001', owner: 'Verge' }
        ]

        return request(app).get("/drones/2")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).not.toEqual(response);
            });
    });
    test('get single owner fail check', () => {
        const response = [
            { "id": 2, type: 1, name: 'X1-002', owner: 'Vegre' }
        ]

        return request(app).get("/drones/2")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).not.toEqual(response);
            });
    });
    test('get single type fail check', () => {
        const response = [
            { "id": 2, type: 0, name: 'X1-002', owner: 'Verge' }
        ]

        return request(app).get("/drones/2")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).not.toEqual(response);
            });
    });
});

/* POST test */
describe('add single drone via post', () => {
    test('add drone', () => {
        return request(app).post('/drones')
            .send({ type: 1, name: 'X1-006', owner: 'Verge' })
            .then(res => {
                expect(res.status).toBe(201);
            });
    });
});

/* GET all test */
describe('get all drones currently in table', () => {
    test('get all drones successfully', () => {
        const response = [
            { "id": 1, type: 0, name: 'X1-001', owner: 'Verge' },
            { "id": 2, type: 1, name: 'X1-002', owner: 'Verge' },
            { "id": 3, type: 1, name: 'X1-006', owner: 'Verge' }
        ]

        return request(app).get("/drones")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).toEqual(response);
            });
    });

    test('fail getting all drones', () => {
        const response = [
            { "id": 1, type: 0, name: 'X1-001', owner: 'Verge' },
            { "id": 2, type: 1, name: 'X1-002', owner: 'Verge' }
        ]

        return request(app).get("/drones")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).not.toEqual(response);
            });
    });

    test('error 404 getting all drones, wrong table', () => {
        return request(app).get("/faketable")
            .then(res => {
                expect(res.status).toBe(404);
            });
    });
});

/* PUT test */
describe('replace a drone entry and check', () => {
    test('replace drone', () => {
        return request(app)
            .put('/drones/1')
            .send({ type: 1, name: 'X1-005', owner: 'Tony' })
            .then(res => {
                expect(res.status).toBe(200);
            });
        
    });

    test('get all drones to check replacement', () => {
        const response = [
            { "id": 1, type: 1, name: 'X1-005', owner: 'Tony' },
            { "id": 2, type: 1, name: 'X1-002', owner: 'Verge' },
            { "id": 3, type: 1, name: 'X1-006', owner: 'Verge' }
        ]

        return request(app).get("/drones")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).toEqual(response);
            });
    });
});

/* PATCH test */
describe('upadate a drone entry and check', () => {
    test('update drone 2 name', () => {
        return request(app)
            .patch('/drones/2')
            .send({ name: 'X1-Patched' })
            .then(res => {
                expect(res.status).toBe(200);
            });
    });
    test('update drone 1 type', () => {
        return request(app)
            .patch('/drones/1')
            .send({ type:0 })
            .then(res => {
                expect(res.status).toBe(200);
            });
    });

    test('update drone 3 owner', () => {
        return request(app)
            .patch('/drones/3')
            .send({ owner:'Strictly' })
            .then(res => {
                expect(res.status).toBe(200);
            });

    });

    test('get all drones to check update', () => {
        const response = [
            { "id": 1, type: 0, name: 'X1-005', owner: 'Tony' },
            { "id": 2, type: 1, name: 'X1-Patched', owner: 'Verge' },
            { "id": 3, type: 1, name: 'X1-006', owner: 'Strictly' }
        ]

        return request(app).get("/drones")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).toEqual(response);
            });
    });
});

/* DELETE test */
describe('delete a drone entry and check', () => {
    test('delete drone', () => {
        return request(app)
            .delete('/drones/1')
            .then(res => {
                expect(res.status).toBe(200);
            });

    });

    test('get all drones to check deletion', () => {
        const response = [
            { "id": 2, type: 1, name: 'X1-Patched', owner: 'Verge' },
            { "id": 3, type: 1, name: 'X1-006', owner: 'Strictly' }
        ]

        return request(app).get("/drones")
            .then(res => {
                expect(res.status).toBe(200);
                expect(res.body).toEqual(response);
            });
    });
});