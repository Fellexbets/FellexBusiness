<template>
  <NavBarComponent></NavBarComponent>
  <MusicPlayer></MusicPlayer>
  <GameChosing v-if="!gamealtStore.gameTimedOngoing && !gamealtStore.gameNormalOngoing"></GameChosing>

  <GameTimed v-if="gamealtStore.gameTimedOngoing"></GameTimed>

  <GameNormal v-if="gamealtStore.gameNormalOngoing"></GameNormal>

  <GameScores v-if="gamealtStore.displayScore && !gamealtStore.gameTimedOngoing && !gamealtStore.gameNormalOngoing"></GameScores>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useGamealtStore, useAuthStore } from "@/stores/"
import { GameChosing, GameNormal, GameTimed, GameScores, MusicPlayer } from "@/components/";

export default defineComponent({
  computed: {
    ...mapStores(useGamealtStore, useAuthStore)
  },
  components: {
    GameChosing, GameNormal, GameScores, GameTimed, MusicPlayer
  }
})
</script>


