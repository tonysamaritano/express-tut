const express = require('express');
const router = express.Router();
const drones = require('../services/drones');

/* GET drones listing */
router.get('/', function (req, res, next) {
    try {
        res.json(drones.getAllDrones());        
    } catch (err) {
        console.error(`Error while getting drones `, err.message);
        responseCode = drones.FindResponseCode(err);
        res.status(responseCode).json({ error: err.message });
        
        // Next passes control to the next handler
        // next('route') bypasses current remaining 
        // route callbacks and jumps to a new route
        next(err);
    }
});

/* GET single drone by id */
router.get('/:id', function (req, res) {
    try {
        res.status(200).json(drones.getSingle(req.params.id));
    } catch(err) {
        console.error(err);
        responseCode = drones.FindResponseCode(err);
        res.status(responseCode).json({ error: err.message });
    }
});

/* DELETE drones */
router.delete('/:id', function (req, res) {
    try {
        res.json(drones.removeSingle(req.params.id));
    } catch (err) {
        console.error(err);
        responseCode = drones.FindResponseCode(err);
        res.status(responseCode).json({ error: err.message });
    }
});

/* PUT drones */
router.put('/:id', function (req, res, next) {
    try {
        res.json(drones.putSingle(req.body, req.params.id));
    } catch (err) {
        console.error(`Error while updating drones `, err.message);
        responseCode = drones.FindResponseCode(err);
        res.status(responseCode).json({ error: err.message });
        next(err);
    }
});

/* PATCH drones */
router.patch('/:id', function (req, res) {
    try {
        res.json(drones.patchSingle(req.body, req.params.id));
    } catch (err) {
        console.error(`Error while updating drones `, err.message);
        responseCode = drones.FindResponseCode(err);
        res.status(responseCode).json({ error: err.message });
        next(err);
    }
});

/* POST drones */
router.post('/', function (req, res, next) {
    try {
        res.json(drones.create(req.body));
    } catch (err) {
        console.error(`Error while adding drones `, err.message);
        responseCode = drones.FindResponseCode(err);
        if(responseCode == 201)
            res.status(responseCode).json({ success: err.message });
        else
            res.status(responseCode).json({ error: err.message });
        next(err);
    }
});

module.exports = router;