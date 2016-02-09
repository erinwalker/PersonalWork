var scene = new THREE.Scene();
var camera = new THREE.PerspectiveCamera(50, window.innerWidth/window.innerHeight, 1, 800);
var renderer = new THREE.WebGLRenderer();
var cubes = new Array();
var rVal, bVal;

renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

for(var i = 0; i < 100; i++)
{
    var material = new THREE.MeshPhongMaterial( {
    color: 0x00ff00,
    ambient: 0x808080,
    specular: 0xffffff,
    shininess: 1,
    reflectivity: 10
    });
    
    var geometry = new THREE.CubeGeometry(.8, .8, .8);
    
    rVal = 1;
    rVal -= (i*.005);
    bVal = 0;
    bVal += (i*.005);
                
    var color = new THREE.Vector3(rVal, 0, bVal);
    
    material.color.setRGB(color.x, color.y, color.z);
    cubes[i] = new THREE.Mesh( geometry, material);
    var xPos = i - 10;
    //cubes[i].position = new THREE.Vector3(xPos, 1, 0);
    cubes[i].translateX(xPos);
    scene.add(cubes[i]);
}

camera.position.z = 140;
camera.position.y = 60;
camera.position.x = 120;

var dirLight = new THREE.DirectionalLight(0xffffff, 0.25);
dirLight.position.set(1, 6, 1);
scene.add(dirLight);

var dirLight = new THREE.DirectionalLight(0xffffff, 0.25);
dirLight.position.set(-4, 1, 1);
scene.add(dirLight);

var dirLight = new THREE.DirectionalLight(0xffffff, 0.25);
dirLight.position.set(6, 5, 1);
scene.add(dirLight);

var dirLight = new THREE.DirectionalLight(0xffffff, 0.25);
dirLight.position.set(1, 5, 6);
scene.add(dirLight);

var dirLight = new THREE.DirectionalLight(0xffffff, 0.25);
dirLight.position.set(1, -4, 6);
scene.add(dirLight);

var dirLight = new THREE.DirectionalLight(0xffffff, 0.25);
dirLight.position.set(1, 1, -4);
scene.add(dirLight);

    //var ambLight = new THREE.AmbientLight(0x505050);
    //scene.add(ambLight);

function main2()
{
    var render = function()
    {
        for(var i = 0; i < cubes.length; i++)
        {
            if(boostArray[i] != null);
            {
                if(boostArray[i] < 0)
                {
                    var value = boostArray[i] * -1;
                    value = 1/value
                    cubes[i].scale.y = value * 10000;   
                }
            }
        }
        
        requestAnimationFrame(render);


        renderer.render(scene, camera);
    };
    render();
}