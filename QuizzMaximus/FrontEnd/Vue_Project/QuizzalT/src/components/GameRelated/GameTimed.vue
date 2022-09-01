<template>
   <div :style="{'background-image': `url('src/assets/ThemeImages/${backgroundName}.png')`}" id="themeImage">
    <div class="container" id="gameDiv">
      <div id="timerDiv">
        <p :style="{'background-color': `${color}`}" class="timer">{{ timer }}</p>
      </div>
      <hr />
      <div class="row" id="questionIndexDiv">
        <span class="questionNumberSpan">
          <h2 class="questionText">Current question: {{ gamealtStore.currentQuestionIndex + 1 }}</h2>
        </span>
        <span class="questionIndexSpan">
          <h2 class="questionText">Total: {{ gamealtStore.quizzDict.length }}</h2>
        </span>
        <span class="questionIndexSpan">
          <h2 class="questionText">Theme: {{ combFilterStore.getQuizzTheme(gamealtStore.getQuizz.quizzId) }}</h2>
        </span>
      </div>
      <hr />

      <div v-if="gamealtStore.currentQuestion.questionImage" class="imageDiv">
        <img :src="`data:image/png;base64,${gamealtStore.currentQuestion.questionImage}`" id="questionImage"/>
      </div>

      <div class="questionDiv">
        <p class="questionName"><strong> {{ gamealtStore.currentQuestion.questionName }} </strong></p>
        <p class="question"><strong> {{ gamealtStore.currentQuestion.questionText }} </strong></p>
      </div>
      <br />

      <div class="row" id="answersDiv">
        <span v-for="item in gamealtStore.currentAnswers" class="col-md-6" :key="item.answerId" id="answerDiv">
          <button @click="gamealtStore.quizzDict.insertChosenAnswer(item)" class="btn_answers" id="answerButtons"
            :style="{ background: gamealtStore.cssAnswerColorByChoice(item) }">{{ item.answerText }}</button>
        </span>
      </div>
      <div class="gameButtonsDiv">
        <button @click="submitAnswer()" class="gameButtons">Submit</button>
        <button @click="gamealtStore.toggleTimedGameOngoing()" class="gameButtons">Stop</button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useGamealtStore, useCombFilterStore } from "@/stores";
import { mapStores } from "pinia";

export default defineComponent({
  data(){
    return {
      timer: {} as number,
      backgroundName: {} as string,
      color: 'green',
      sendAnswer: false
    }
  },
  computed: {
    ...mapStores(useGamealtStore, useCombFilterStore)
  },
  methods: {
    startTimer(){
      this.timer = 10;
    },
    background(){
      this.backgroundName = this.combFilterStore.getQuizzTheme(this.gamealtStore.getQuizz.quizzId);
      if(this.backgroundName == 'Misc'){
        this.backgroundName = this.gamealtStore.getCurrentQuestionTheme();
      }
    },
    submitAnswer(){
      this.sendAnswer = true;
      this.background();
    },
    clockTimeOut(){
      if(this.sendAnswer){
        if(this.gamealtStore.currentQuestionIndex + 1 == this.gamealtStore.quizzDict.length){
          this.gamealtStore.submitGameTimed();
        } else {       
          this.sendAnswer = false;   
          this.gamealtStore.nextQuestion();
          this.background();
          this.startTimer();
          this.color = 'green';
        }
      } else {
        if(this.timer > 0){
          setTimeout(() => {this.timer--;}, 1000);
          if(this.timer < 5){
            this.color = 'red';
          }  
        } else {
          if(this.gamealtStore.currentQuestionIndex + 1 == this.gamealtStore.quizzDict.length){
            this.gamealtStore.submitGameTimed();
          } else {          
            this.gamealtStore.nextQuestion();
            this.background();
            this.startTimer();
            this.color = 'green';
          }
        }
      }
    }
  },
  mounted(){
    this.startTimer();
    this.background();
  },
  watch: {
    timer: {
      handler: 'clockTimeOut'
    }
  }
})
</script>
