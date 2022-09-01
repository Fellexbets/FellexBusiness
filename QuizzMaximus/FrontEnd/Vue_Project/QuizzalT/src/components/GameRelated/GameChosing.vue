<template>
    <div class="container" v-if="authStore.user" id="gameChooseContainer">
    <h4 class="text-center">Let's Play!</h4> <br />
    <GameChooseMode v-if="chooseMode" @game-mode="(mode : string) => startGame(mode)"></GameChooseMode>
    <GameRandomChooseMode v-if="chooseModeRandom" @game-mode-random="(mode : string) => startGameRandom(mode)"></GameRandomChooseMode>
    <h5 class="text-center">Chose a quizz to play</h5>
    <div class="scrollable">
      <table class="w-100" id="quizzSelect">
        <thead>
          <th>Quizz name</th>
          <th>Created by</th>
          <th>Theme</th>
          <th>Questions num</th>
          <th>Difficulty avg</th>
          <th>Points</th>
        </thead>
        <tbody>
          <tr v-for="item in quizzStore.getPublishedQuizzes" :key="item.quizzId">
            <td>{{ item.quizzName }}</td>
            <td>{{ userStore.getUserNameById(item.userId) }}</td>
            <td>{{ combFilterStore.getQuizzTheme(item.quizzId)}}</td>
            <td>{{ quizzQuestionStore.getQuestionsNumberOfQuizzId(item.quizzId) }}</td>
            <td>{{ combFilterStore.getQuizzDifficultyAverage(item.quizzId)}}</td>
            <td>{{ combFilterStore.getQuizzTotalPoints(item.quizzId)}}</td>
            <td><button @click="gameChoose(item.quizzId)">Play</button></td>
          </tr>
        </tbody>
      </table>
    </div>
    <br />

    <h5 class="text-center">Or play a random quizz</h5>
    <div class="randomQuizzButtonDiv">
      <button @click="gameChooseRandom()">New Random Quizz</button>
    </div>
    <br /><br /><br />
  </div>

</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useGamealtStore, useCombFilterStore, useQuizzStore, useQuizzQuestionStore, useAuthStore, useUserStore } from "@/stores";
import GameChooseMode from "./GameChooseMode.vue";
import GameRandomChooseMode from "./GameRandomChooseMode.vue";

export default defineComponent({
    data() {
        return {
            gameMode: "",
            chooseMode: false,
            chooseModeRandom: false,
            quizzToPlay: {} as number
        };
    },
    computed: {
        ...mapStores(useGamealtStore, useCombFilterStore, useQuizzStore, useQuizzQuestionStore, useAuthStore, useUserStore)
    },
    methods:{
      gameChoose(quizzId : number){
        this.chooseMode = true;
        this.quizzToPlay = quizzId;
      },
      startGame(mode : string){
        if(mode == 'cancel'){
            this.chooseMode = false;
        } else {
          this.chooseMode = false;
          this.gamealtStore.playChosenQuizz(this.quizzToPlay, mode);
        }
      },
      gameChooseRandom(){
        this.chooseModeRandom = true;
      },
      startGameRandom(mode : string){
        if(mode == 'cancel'){
          this.chooseModeRandom = false;
        } else {
          this.chooseModeRandom = false;
          this.gamealtStore.playRandomQuizz(mode);
        }
      }
    },
    components: {
      GameChooseMode,
      GameRandomChooseMode
  }
})
</script>
