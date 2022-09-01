<template>
  <div class="container">
    <h4>Create Achievement</h4>

    <form @submit.prevent="achievementCreate()">
      <div class="mb-3">
        <label for="inputAchievementCreateThemeId" class="form-label">ThemeId</label>
        <input v-model="achievement.themeId" type="themeId" class="form-control" id="inputAchievementCreateThemeId">
      </div>
      <div class="mb-3">
        <label for="inputAchievementCreateUserId" class="form-label">UserId</label>
        <input v-model="achievement.userId" type="userId" class="form-control" id="inputAchievementCreateUserId">
      </div>
      <div class="mb-3">
        <label for="inputAchievementCreateGainedPoints" class="form-label">GainedPoints</label>
        <input v-model="achievement.gainedPoints" type="gainedPoints" class="form-control" id="inputAchievementCreateGainedPoints">
      </div>
      
      <button type="submit">Create</button>
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
      achievement: {} as IAchievement
    }
  },
  methods: {
    achievementCreate() {
      this.achievementStore.apiCreate(this.achievement);
      this.$emit('changePage', 'AchievementIndexComponent');
    },
    back() {
      this.$emit('changePage', 'AchievementIndexComponent');
    }
  },
  mounted(){
    this.achievement = this.achievementStore.getCurrentAchievement;
  },
  computed: {
    ...mapStores(useAchievementStore)
  }
})
</script>