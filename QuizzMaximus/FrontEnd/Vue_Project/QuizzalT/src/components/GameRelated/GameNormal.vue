<template>
  <div :style="{'background-image': `url('src/assets/ThemeImages/${backgroundName}.png')`}" id="themeImage">
    <div class="container" id="gameDiv">
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
        <div v-for="item in gamealtStore.currentAnswers" class="col-md-6" :key="item.answerId" id="answerDiv">
          <button @click="gamealtStore.quizzDict.insertChosenAnswer(item)" class="btn_answers" id="answerButtons"
            :style="{ background: gamealtStore.cssAnswerColorByChoice(item) }">{{ item.answerText }}</button>
        </div>
      </div>
      <hr />

      <div class="gameButtonsDiv">
        <button @click="prevQuestion()" class="gameButtons">Prev</button>
        <button @click="nextQuestion()" class="gameButtons">Next</button>
      </div>
      <hr />

      <div class="gameButtonsDiv">
        <button @click="gamealtStore.submitGameNormal()" class="gameButtons">Submit</button>
        <button @click="gamealtStore.quizzDict.clearAllChosenAnswers()" class="gameButtons">Reset</button>
        <button @click="gamealtStore.toggleNormalGameOngoing()" class="gameButtons">Stop</button>
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
        backgroundName: ''
    }
  },
  methods: {
    background(){
      this.backgroundName = this.combFilterStore.getQuizzTheme(this.gamealtStore.getQuizz.quizzId);
      if(this.backgroundName == 'Misc'){
        this.backgroundName = this.gamealtStore.getCurrentQuestionTheme();
      }
    },
    prevQuestion(){
      this.gamealtStore.prevQuestion();
      this.background();
    },
    nextQuestion(){
      this.gamealtStore.nextQuestion();
      this.background();
    }
  },
  mounted(){
    this.background();
  },
  computed: {
    ...mapStores(useGamealtStore, useCombFilterStore)
  }
})
</script>

