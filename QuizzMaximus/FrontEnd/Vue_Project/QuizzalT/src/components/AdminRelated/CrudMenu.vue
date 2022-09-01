<template>
  <div class="container">
    <h5 class="text-center">Admin model CRUD views</h5>
    <div class="adminButtonsDiv">
      <button @click="$router.push('/achievement')" class="adminButtons">Achievements</button>
      <button @click="$router.push('/answer')" class="adminButtons">Answers</button>
      <button @click="$router.push('/playedQuestion')" class="adminButtons">PlayedQuestions</button>
      <button @click="$router.push('/playedQuizz')" class="adminButtons">PlayedQuizzes</button>
      <button @click="$router.push('/question')" class="adminButtons">Questions</button>
      <button @click="$router.push('/quizz')" class="adminButtons">Quizzes</button>
      <button @click="$router.push('/quizzQuestion')" class="adminButtons">QuizzQuestions</button>
      <button @click="$router.push('/relation')" class="adminButtons">Relations</button>
      <button @click="$router.push('/theme')" class="adminButtons">Themes</button>
      <button @click="$router.push('/user')" class="adminButtons">Users</button>
      <button @click="$router.push('/userRelation')" class="adminButtons">UserRelations</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({

})
</script>

