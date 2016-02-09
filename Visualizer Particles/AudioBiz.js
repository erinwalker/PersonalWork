var boost = 0;
var boostArray = new Array();
var url = 'videos/LoveIsABattlefield.mp4';
var scriptProcessor;
var begin = false;
var context;
function main()
{
   //Creates new Audio Context and request
   context = new AudioContext(),
   request = new XMLHttpRequest();
   var analyser;
   var freqData = new Array();
   request.open('GET', url, true);
   request.responseType = 'arraybuffer';
   request.onload = function () 
   {
    var undecodedAudio = request.response;
                
    context.decodeAudioData(undecodedAudio, function (buffer) 
    {
        //Setting up the script processor to have one input, one output, and a bitrate of 8192
        scriptProcessor = context.createScriptProcessor(2048, 1, 1);
        scriptProcessor.buffer = buffer;
        scriptProcessor.connect(context.destination);
        
        analyser = context.createAnalyser();
        //STC averaging between current buffer and last buffer the Analyser processes
        analyser.smoothingTimeConstant = 0.6;
        analyser.fftSize = 512; 
        
        var sourceBuffer = context.createBufferSource();
        sourceBuffer.buffer = buffer;
        sourceBuffer.loop = true;
        
        //Source(our music file) -> Analyser(gets frequency info) -> Destination(our speakers)
        sourceBuffer.connect(analyser);
        analyser.connect(scriptProcessor);
        sourceBuffer.connect(context.destination);
        
        scriptProcessor.onaudioprocess = function(e)
        {
            freqData = new Float32Array(analyser.frequencyBinCount);
            analyser.getFloatFrequencyData(freqData);
            //boost = 0;
            boostArray = new Array();
            for (var i = 0; i < freqData.length; i++)
                {
                    boostArray[i]= freqData[i];
                    //boost += freqData[i];
                }
            boost = boost / freqData.length;
        };

        sourceBuffer.start(context.currentTime);
        begin = true;

    });
   };
        request.send();
}


function switchSource()
{
    if(switchVideo == 0)
    {
        url = "videos/LoveIsABattlefield.mp4";
        videoImage.width = 480;
        videoImage.height = 204;
        switchVideo++;
    }
    else if(switchVideo == 1)
    {
        url = "videos/TotalEclipseOfTheHeart.mp4";
        videoImage.width = 480;
        videoImage.height = 204;
        switchVideo++;
    }
    else if(switchVideo == 2)
    {
        url = "videos/WhiteFlag.mp4";
        videoImage.width = 600;
        videoImage.height = 400;
        switchVideo = 0;
    }
    context.close();
    main();
}