<template>
  <div class="popupScore">
    <div class="popupInnerScore">
      <div class="container" >
        <div class="scrollablePoints">
        <h5 class="pointsTable">Your Score:</h5>

        <table class="w-50 mx-auto">
          <thead>
            <tr>
              <th></th>
              <th class="pointsTable">Questions</th>
              <th class="pointsTable">Points</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td class="pointsTable">Total:</td>
              <td class="pointsTable">{{ gamealtStore.quizzDict.length }} (100%)</td>
              <td class="pointsTable">{{ gamealtStore.quizzDict.totalPoints }} (100%)</td>
            </tr>
            <tr>
              <td class="pointsTable">Correct:</td>
              <td class="pointsTable">{{ gamealtStore.getNumRightAnswers }} ({{ gamealtStore.getPercentRightAnswers }}%)</td>
              <td class="pointsTable">{{ gamealtStore.getPointsObtained }} ({{ gamealtStore.getPercentPointsObtained }}%)</td>
            </tr>
            <tr>
              <td class="pointsTable">Incorrect:</td>
              <td class="pointsTable">{{ gamealtStore.getNumWrongAnswers }} ({{ gamealtStore.getPercentWrongedAnswers }}%)</td>
              <td class="pointsTable">{{ gamealtStore.getPointsWronged }} ({{ gamealtStore.getPercentPointsWronged }}%)</td>
            </tr>
            <tr>
              <td class="pointsTable">Unanswered:</td>
              <td class="pointsTable">{{ gamealtStore.getNumNotAnswered }} ({{ gamealtStore.getPercentNotAnswered }}%)</td>
              <td class="pointsTable">{{ gamealtStore.getPointsNotAnswered }} ({{ gamealtStore.getPercentPointsNotAnswered }}%)</td>
            </tr>
          </tbody>
        </table>
        <br />

        <div>
          <table class="w-75 mx-auto">
            <thead>
              <tr>
                <th class="pointsTable"> Question Name </th>
                <th class="pointsTable"> Points </th>
                <th class="pointsTable"> Your Answer </th>
                <th class="pointsTable"> Right Answer </th>
                <th> </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in gamealtStore.quizzDict.gameQuestions">
                <td class="pointsTable"> {{ item.question.questionName }} </td>
                <td class="pointsTable"> {{ item.question.difficulty * 10 }} </td>
                <td class="pointsTable"> {{ item.chosenAnswer?.answerText }} </td>
                <td class="pointsTable"> {{ item.getRightAnswer().answerText }} </td>
                <td class="pointsTable"> {{ item.gotItRightDingbat() }} </td>
              </tr>
            </tbody>
          </table>
          <br />
        </div>

        <button @click="gamealtStore.toggleGameScores()">Hide Score</button>
      </div> 

      <br /><br /><br />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useGamealtStore } from "@/stores";
import { mapStores } from "pinia";

export default defineComponent({
  computed: {
    ...mapStores(useGamealtStore)
  }
})
</script>
