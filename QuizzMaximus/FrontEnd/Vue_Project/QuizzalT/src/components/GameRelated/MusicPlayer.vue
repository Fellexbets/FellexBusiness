<template>
  <div class="container" id="musicDiv">
    <span class="audiospan">
    <audio id="player" src="src/assets/music/game.mp3" autoplay></audio>
      <div id="musicBackground">
          <button class="musicButton" onclick="document.getElementById('player').play()">Play</button> 
          <button class="musicButton" onclick="document.getElementById('player').pause()">Pause</button> 
          <button class="musicButton" onclick="document.getElementById('player').volume += 0.1">Vol +</button> 
          <button class="musicButton" onclick="document.getElementById('player').volume -= 0.1">Vol -</button> 
      </div>
      </span>
      <span class="audiospan">
      <svg class="sound-equalizer-animated lds-equalizer" width="100%" height="100%" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid" style="background: none;"><g transform="rotate(180 50 50)">
                    <rect class="eq-bars" ng-attr-x="{{20 - config.width/2}}" y="15" ng-attr-width="{{config.width}}" height="47.4493" fill="#00e3e8" x="11" width="18">
                        <animate attributeName="height" calcMode="spline" values="50;75;10;50" times="0;0.33;0.66;1" ng-attr-dur="{{config.speed}}" keySplines="0.5 0 0.5 1;0.5 0 0.5 1;0.5 0 0.5 1" repeatCount="indefinite" begin="0s" dur="1">
                        </animate></rect><rect class="eq-bars" ng-attr-x="{{40 - config.width/2}}" y="15" ng-attr-width="{{config.width}}" height="10.1317" fill="#00e3e8" x="32" width="18">
                        <animate attributeName="height" calcMode="spline" values="50;75;10;50" times="0;0.33;0.66;1" ng-attr-dur="{{config.speed}}" keySplines="0.5 0 0.5 1;0.5 0 0.5 1;0.5 0 0.5 1" repeatCount="indefinite" begin="-0.75s" dur="1">
                        </animate></rect><rect class="eq-bars" ng-attr-x="{{60 - config.width/2}}" y="15" ng-attr-width="{{config.width}}" height="64.8024" fill="#00e3e8" x="52" width="18">
                        <animate attributeName="height" calcMode="spline" values="50;75;10;50" times="0;0.33;0.66;1" ng-attr-dur="{{config.speed}}" keySplines="0.5 0 0.5 1;0.5 0 0.5 1;0.5 0 0.5 1" repeatCount="indefinite" begin="-0.5s" dur="1">
                        </animate></rect><rect class="eq-bars" ng-attr-x="{{80 - config.width/2}}" y="15" ng-attr-width="{{config.width}}" height="64.9211" fill="#00e3e8" x="72" width="18">
                        <animate attributeName="height" calcMode="spline" values="50;75;10;50" times="0;0.33;0.66;1" ng-attr-dur="{{config.speed}}" keySplines="0.5 0 0.5 1;0.5 0 0.5 1;0.5 0 0.5 1" repeatCount="indefinite" begin="-0.25s" dur="1">
                        </animate></rect></g>
                <rect width="100" height="5" x="0" y="10" style="fill:rgba(28,27,45,1);"></rect>
                <rect width="100" height="5" x="0" y="26" style="fill:rgba(28,27,45,1);"></rect>
                <rect width="100" height="5" x="0" y="42" style="fill:rgba(28,27,45,1);"></rect>
                <rect width="100" height="5" x="0" y="56" style="fill:rgba(28,27,45,1);"></rect>
                <rect width="100" height="5" x="0" y="70" style="fill:rgba(28,27,45,1);"></rect>
            </svg>
      </span>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({})
</script>