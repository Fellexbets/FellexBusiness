<template>
  <div class="container">
    <h4>Edit Achievement</h4>

    <form @submit.prevent="achievementUpdate()">
      <div class="mb-3">
        <label for="inputAchievementEditThemeId" class="form-label">Current ThemeId: {{ achievementToDisplayCurrent.themeId }} </label>
        <input v-model="achievement.themeId" type="themeId" class="form-control" id="inputAchievementEditThemeId">
      </div>
      <div class="mb-3">
        <label for="inputAchievementEditUserId" class="form-label">Current UserId: {{ achievementToDisplayCurrent.userId }}</label>
        <input v-model="achievement.userId" type="userId" class="form-control" id="inputAchievementEditUserId">
      </div>
      <div class="mb-3">
        <label for="inputAchievementEditGainedPoints" class="form-label">Current GainedPoints: {{ achievementToDisplayCurrent.gainedPoints }}</label>
        <input v-model="achievement.gainedPoints" type="gainedPoints" class="form-control" id="inputAchievementEditGainedPoints">
      </div>

      <button type="submit">Update</button>
      <button @click="back()">Back</button>
    </form>

  </div>
</template>


<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useAchievementStore } from "@/stores/";
import type { IAchievement } from "@/models/";

export default defineComponent({
  data() {
    return {
      achievement: {} as IAchievement,
      achievementToDisplayCurrent: {} as IAchievement
    }
  },
  methods: {
    achievementUpdate() {
      this.achievementStore.apiUpdate(this.achievement);
      this.$emit('changePage', 'AchievementDetailsComponent');
    },
    back() {
      this.$emit('changePage', 'AchievementIndexComponent');
    }
  },
  mounted() {
    this.achievement = this.achievementStore.getCurrentAchievement;
    this.achievementToDisplayCurrent.themeId = this.achievement.themeId;
    this.achievementToDisplayCurrent.userId = this.achievement.userId;
    this.achievementToDisplayCurrent.gainedPoints = this.achievement.gainedPoints;
  },
  computed: {
    ...mapStores(useAchievementStore)
  }
})
</script>