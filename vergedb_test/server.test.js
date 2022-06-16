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

test('get first entry', () => {
    seedDb(db);
    const res = request(server).get('/1');
    const response = [
        { type: 0, name: 'X1-001', owner: 'Verge' }
    ]

    expect(res.status).toBe(200);
    expect(res.body).toEqual(response);
});

