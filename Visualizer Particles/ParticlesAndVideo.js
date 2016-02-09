//var SEPARATION = 50, AMOUNTX = 30, AMOUNTY = 1, AMOUNTUP = 5;
//var SEPARATION = 180, AMOUNTX = 40, AMOUNTY = 1, AMOUNTUP = 3;
var SEPARATION = 90, AMOUNTX = 80, AMOUNTY = 1, AMOUNTUP = 2;
			var container, stats;
			var camera, scene, renderer;

			var particles, particle, count = 0;

			var mouseX = 0, mouseY = 0;

			var windowHalfX = window.innerWidth / 2;
			var windowHalfY = window.innerHeight / 2;
            
            var clicked = false;

            var video, videoImage, videoImageContext, videoTexture;

			init();
			animate();
            //render();
			function init() {

				container = document.createElement( 'div' );
				document.body.appendChild( container );

                //camera = new THREE.OrthographicCamera(75, window.innerWidth / window.innerHeight, 1, 10000 );
				camera = new THREE.PerspectiveCamera( 75, window.innerWidth / window.innerHeight, 1, 10000 );
				camera.position.z = 2000;
                camera.position.y = 3400;

				scene = new THREE.Scene();
                
				particles = new Array();
                
                // create the video element
                video = document.createElement( 'video' );
                //video.src = "videos/LoveIsABattlefield.mp4";
                video.src = url;
                video.load(); // must call after setting/changing source
                video.muted= true;
                //video.play();

                videoImage = document.createElement( 'canvas' );
                videoImage.width = 480;
                videoImage.height = 204;
                videoImageContext = videoImage.getContext( '2d' );
                // background color if no video present
                videoImageContext.fillStyle = '#000000';
                videoImageContext.fillRect( 0, 0, videoImage.width, videoImage.height );
                videoTexture = new THREE.Texture( videoImage );
                videoTexture.minFilter = THREE.LinearFilter;
                videoTexture.magFilter = THREE.LinearFilter;

                var movieMaterial = new THREE.MeshBasicMaterial( { map: videoTexture, overdraw: true, side:THREE.DoubleSide } );
                // the geometry on which the movie will be displayed;
                // 		movie image will be scaled to fit these dimensions.
                var movieGeometry = new THREE.PlaneGeometry( 9000, 4500, 4, 4 );
                
                //Screen 1 Front
                var movieScreen = new THREE.Mesh( movieGeometry, movieMaterial );
                movieScreen.position.set(0, 3100, -500);
                scene.add(movieScreen);
                
                /*//Screen 2 Right
                 var movieScreen2 = new THREE.Mesh( movieGeometry, movieMaterial );
                movieScreen2.position.set(1500, 0, 200);
                movieScreen2.rotation.set(0, 90, 0);
                scene.add(movieScreen2);
                
                //Screen 3 Left
                var movieScreen3 = new THREE.Mesh( movieGeometry, movieMaterial );
                movieScreen3.position.set(-1500, 0, 200);
                movieScreen3.rotation.set(0, -90, 0);
                scene.add(movieScreen3);
                
                //Screen 4 Top
                var movieScreen4 = new THREE.Mesh( movieGeometry, movieMaterial );
                movieScreen4.position.set(0, 1000, 200);
                movieScreen4.rotation.set(-90, 0, 0);
                scene.add(movieScreen4);
                
                //Screen 5 Bottom
                var movieScreen5 = new THREE.Mesh( movieGeometry, movieMaterial );
                movieScreen5.position.set(0, -1000, 200);
                movieScreen5.rotation.set(90, 0, 0);
                scene.add(movieScreen5);*/
                
				var PI2 = Math.PI * 2;
				var materialRed = new THREE.SpriteCanvasMaterial( {

					color: 0xCC0000,
					program: function ( context ) {

						context.beginPath();
						context.arc( 0, 0, 0.5, 0, PI2, true );
						context.fill();

					}

				} );
                var materialOrange = new THREE.SpriteCanvasMaterial( {

					color: 0xFF6600,
					program: function ( context ) {

						context.beginPath();
						context.arc( 0, 0, 0.5, 0, PI2, true );
						context.fill();

					}

				} );
                var materialYellow = new THREE.SpriteCanvasMaterial( {

					color: 0xFFFF00,
					program: function ( context ) {

						context.beginPath();
						context.arc( 0, 0, 0.5, 0, PI2, true );
						context.fill();

					}

				} );
                var materialGreen = new THREE.SpriteCanvasMaterial( {

					color: 0x00CC00,
					program: function ( context ) {

						context.beginPath();
						context.arc( 0, 0, 0.5, 0, PI2, true );
						context.fill();

					}

				} );
                var materialBlue = new THREE.SpriteCanvasMaterial( {

					color: 0x0000FF,
					program: function ( context ) {

						context.beginPath();
						context.arc( 0, 0, 0.5, 0, PI2, true );
						context.fill();

					}

				} );
                var materialPurple = new THREE.SpriteCanvasMaterial( {

					color: 0x6600CC,
					program: function ( context ) {

						context.beginPath();
						context.arc( 0, 0, 0.5, 0, PI2, true );
						context.fill();

					}

				} );


				var i = 0;
                for(var iz = 0; iz < AMOUNTUP; iz ++ )
                {
				for ( var ix = 0; ix < AMOUNTX; ix ++ ) {

					for ( var iy = 0; iy < AMOUNTY; iy ++ ) {
                        var colorNum = Math.floor(Math.random() * (6 - 1 + 1)) + 1;
                        if(colorNum == 1)
                        {
                            particle = particles[ i ++ ] = new THREE.Sprite( materialRed );
                        }
                        else if(colorNum == 2)
                        {
                            particle = particles[ i ++ ] = new THREE.Sprite( materialOrange );
                        }
                        else if(colorNum == 3)
                        {
                            particle = particles[ i ++ ] = new THREE.Sprite( materialYellow );
                        }
                        else if(colorNum == 4)
                        {
                            particle = particles[ i ++ ] = new THREE.Sprite( materialGreen );
                        }
                        else if(colorNum == 5)
                        {
                            particle = particles[ i ++ ] = new THREE.Sprite( materialBlue );
                        }
                        else if(colorNum == 6)
                        {
                            particle = particles[ i ++ ] = new THREE.Sprite( materialPurple );
                        }
						particle.position.x = ix * SEPARATION - ( ( AMOUNTX * SEPARATION ) / 2 );
						//particle.position.z = iy * SEPARATION - ( ( AMOUNTY * SEPARATION ) / 2 );
                        particle.position.z = -500;
                        //particle.position.y = iz * SEPARATION -(100);
                        particle.position.y = 0;
						scene.add( particle );

					}
				}
                }

				renderer = new THREE.CanvasRenderer();
				renderer.setPixelRatio( window.devicePixelRatio );
				renderer.setSize( window.innerWidth, window.innerHeight );
				container.appendChild( renderer.domElement );
                
                //renderer.setClearColor( 0xffffff, 1);
                
				stats = new Stats();
				stats.domElement.style.position = 'absolute';
				stats.domElement.style.top = '0px';
				container.appendChild( stats.domElement );

				document.addEventListener( 'mousemove', onDocumentMouseMove, false );
				document.addEventListener( 'touchstart', onDocumentTouchStart, false );
				document.addEventListener( 'touchmove', onDocumentTouchMove, false );
                document.addEventListener('click', click);

				//

				window.addEventListener( 'resize', onWindowResize, false );

			}
            
            function click()
            {
                clicked = !clicked;
            }

			function onWindowResize() {

				windowHalfX = window.innerWidth / 2;
				windowHalfY = window.innerHeight / 2;

				camera.aspect = window.innerWidth / window.innerHeight;
				camera.updateProjectionMatrix();

				renderer.setSize( window.innerWidth, window.innerHeight );

			}

			//

			function onDocumentMouseMove( event ) {

				mouseX = event.clientX - windowHalfX;
				mouseY = event.clientY - windowHalfY;

			}

			function onDocumentTouchStart( event ) {

				if ( event.touches.length === 1 ) {

					event.preventDefault();

					mouseX = event.touches[ 0 ].pageX - windowHalfX;
					mouseY = event.touches[ 0 ].pageY - windowHalfY;

				}

			}

			function onDocumentTouchMove( event ) {

				if ( event.touches.length === 1 ) {

					event.preventDefault();

					mouseX = event.touches[ 0 ].pageX - windowHalfX;
					mouseY = event.touches[ 0 ].pageY - windowHalfY;

				}

			}

            var switchVideo = 1;
            var enteredIF = false;
            
			function animate() {

				requestAnimationFrame( animate );

                if(clicked == false)
                {
                    enteredIF = false;
                    if(begin == true)
                        video.play();
				    render();
                }
                else if(clicked == true && enteredIF == false)
                {
                    video.pause();
                    switchSource();
                     video.src = url;
                    /*if(switchVideo == 0)
                    {
                        video.src = "videos/LoveIsABattlefield.mp4";
                        videoImage.width = 480;
                        videoImage.height = 204;
                        switchVideo++;
                    }
                    else if(switchVideo == 1)
                    {
                        video.src = "videos/TotalEclipseOfTheHeart.mp4";
                        videoImage.width = 480;
                        videoImage.height = 204;
                        switchVideo++;
                    }
                    else 
                    if(switchVideo == 2)
                    {
                        //video.src = "videos/WhiteFlag.mp4";
                        videoImage.width = 600;
                        videoImage.height = 400;
                    }*/
                    enteredIF = true;
                }
				stats.update();
			}

			function render() {

				//camera.position.x += ( mouseX - camera.position.x ) * .05;
                //camera.position.z += ( mousey - camera.position.z ) * .05;
				//camera.rotation.y +=  0.8;
				//camera.lookAt( scene.position );

				var i = 0;

                for ( var iz = 0; iz < AMOUNTUP; iz ++ ) {
                    for ( var ix = 0; ix < AMOUNTX; ix ++ ) {
                        for ( var iy = 0; iy < AMOUNTY; iy ++ ) {

                            particle = particles[ i++ ];
                            //particle.position.y = ( Math.sin( ( ix + count ) * 0.3 ) * 50 ) +
                                //( Math.sin( ( iy + count ) * 0.5 ) * 50 ) + (iz* SEPARATION);
                            //particle.scale.x = particle.scale.y = ( Math.cos( ( ix + count ) * 0.3 ) + 1 ) * 4 +
                                //( Math.sin( ( iy + count ) * 0.5 ) + 1 ) * 4;
                            
                            if(boostArray[i] != null);
                            {
                                if(boostArray[i] < 0)
                                {
                                    var value = boostArray[i] * -1;
                                    value = 1/value
                                    //cubes[i].scale.y = value * 10000;  
                                    particle.position.y = value * 190000;
                                    particle.scale.x = particle.scale.y = value * 5000;
                                }
                            }

                        }
                    }
                }
                
                //Video
                if ( video.readyState === video.HAVE_ENOUGH_DATA ) 
                {
                    videoImageContext.drawImage( video, 0, 0 );
                    if ( videoTexture ) 
                        videoTexture.needsUpdate = true;
                }
                
                
				renderer.render( scene, camera );

				count += 0.1;

			}
