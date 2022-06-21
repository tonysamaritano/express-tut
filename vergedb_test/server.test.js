const { server } = require('./server');
const sqlite3 = require('better-sqlite3');
const request = require('supertest');
const db = new sqlite3(':memory:');
beforeAll(() => {
    process.env.NODE_ENV = 'test';
});

const seedDb = db => {
    db.prepare(`CREATE TABLE IF NOT EXISTS drones(id INTEGER PRIMARY KEY AUTOINCREMENT,
        type INTEGER NOT NULL,
        name TEXT NOT NULL UNIQUE,
        owner TEXT NOT NULL)`).run();
    db.prepare('DELETE FROM drones').run();
    const stmt = db.prepare('INSERT INTO drones (type, name, owner) VALUES (?, ?, ?)');
    stmt.run(0, 'X1-001', 'Verge');
    stmt.run(1, 'X1-002', 'Verge');
    stmt.run(0, 'X1-003', 'Image');
    stmt.run(1, 'X1-004', 'Image');
    stmt.run(0, 'X1-005', 'Verge');
    // stmt.finalize();
}

/* GET Single id test*/
test('get first entry', () => {
    seedDb(db);
    const res = request(server).get('/1');
    const response = [
        { type: 0, name: 'X1-001', owner: 'Verge' }
    ]

    expect(res.status).toBe(200);
    expect(res.body).toEqual(response);
});

// /* POST test */
// test('add drone', () => {
//     db.serialize(async () => {
//         seedDb(db);
//         await request(server)
//             .post('/')
//             .send({ type: 1, name: 'X1-006', owner: 'Verge' });
//         const res = await request(server).get('/');
//         const response = [
//             { type: 0, name: 'X1-001', owner: 'Verge' },
//             { type: 1, name: 'X1-002', owner: 'Verge' },
//             { type: 0, name: 'X1-003', owner: 'Verge' },
//             { type: 1, name: 'X1-004', owner: 'Image' },
//             { type: 0, name: 'X1-005', owner: 'Image' },
//             { type: 1, name: 'X1-006', owner: 'Verge' }
//         ]
//         expect(res.status).toBe(200);
//         expect(res.body).toEqual(response);
//     })
// });

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