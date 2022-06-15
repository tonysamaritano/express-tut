const express = require('express');
const router = express.Router();
const drones = require('../services/drones');

/* GET drones listing. */
router.get('/', function (req, res, next) {
    try {
        //Commands here
        res.json(drones.getMultiple(req.query.page));
        
    } catch (err) {
        console.error(`Error while getting drones `, err.message);
        next(err);
    }
});

router.get('/:id', function (req, res) {
    res.json(drones.getSingle(req.params.id));
});

router.delete('/:id', function (req, res) {
    res.json(drones.removeSingle(req.params.id));
});

/* POST drones */
router.post('/', function (req, res, next) {
    try {
        //Commands here
        console.log(req.params);
        console.log(req.body);
        res.json(drones.create(req.body));
    } catch (err) {
        console.error(`Error while adding drones `, err.message);
        next(err);
    }
});

module.exports = router;