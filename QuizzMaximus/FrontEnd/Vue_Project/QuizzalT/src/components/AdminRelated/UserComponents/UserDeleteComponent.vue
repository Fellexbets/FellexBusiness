<template>
  <div class="container">
    <h4>Delete User</h4>
    <table class="table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Username</th>
          <th>Email</th>
          <th>Role</th>
          <th>Date Created</th>
          <th>Password</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td> {{ user.userId }} </td>
          <td> {{ user.username }} </td>
          <td> {{ user.email }} </td>
          <td> {{ user.role }} </td>
          <td> {{ user.dateCreated }} </td>
          <td> {{ user.password }} </td>
        </tr>
      </tbody>
    </table>
    <p> Are you sure?</p>
    <button @click="userDelete(user.userId)">Delete</button>
    <button @click="back()">Back</button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapStores } from "pinia";
import { useUserStore } from "@/stores/";
import type { IUser } from "@/models/";

export default defineComponent({
  data() {
    return {
      user: {} as IUser
    }
  },
  methods: {
    userDelete(userId: number){
        this.userStore.apiDelete(userId);
        this.$emit('changePage', 'UserIndexComponent');
      },
      back(){
        this.$emit('changePage', 'UserIndexComponent');
      }
  },
  mounted(){
    this.user = this.userStore.getCurrentUser;
  },
  computed:{
    ...mapStores(useUserStore)
  },
  components: {
  }
})
</script>