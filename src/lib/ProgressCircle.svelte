<div class="h-24 w-24 relative m-auto">
    <div class="loader inline-block">
        <svg class="circular" viewBox="0 0 100 100">
        <circle class="path" cx="50" cy="50" r="30" fill="none" stroke-width="10" stroke-miterlimit="10"></circle>
        </svg>
    </div>
</div>

<style>
.loader{
  position: absolute;
  width: 100%;
  height: 100%;
  top: 50%;
  left: 50%;
  transform: translate(-50%,-50%);
}

.circular{
  animation: rotate 2s linear infinite;
  height: 100%;
  position: relative;
  width: 100%;
}

.path {
  stroke-dasharray: 1,200;
  stroke-dashoffset: 0;
  stroke:#B6463A;
  animation:
   dash 1.5s ease-in-out infinite,
   color 6s ease-in-out infinite
  ;
  stroke-linecap: round;
}

@keyframes rotate{
 100%{
  transform: rotate(360deg);
 }
}
@keyframes dash{
 0%{
  stroke-dasharray: 1,200;
  stroke-dashoffset: 0;
 }
 50%{
  stroke-dasharray: 89,200;
  stroke-dashoffset: -35;
 }
 100%{
  stroke-dasharray: 89,200;
  stroke-dashoffset: -124;
 }
}
@keyframes color{
  100%, 0%{
    stroke: #d62d20;
  }
  40%{
    stroke: #0057e7;
  }
  66%{
    stroke: #008744;
  }
  80%, 90%{
    stroke: #ffa700;
  }
}
</style>
