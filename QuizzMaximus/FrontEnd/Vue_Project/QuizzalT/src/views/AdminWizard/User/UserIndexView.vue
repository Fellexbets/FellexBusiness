<template>
    <div class="container">
      <ModelTitle title="User Index" />
      <table class="table">
        <thead>
          <tr>
            <th>Id</th>
            <th>Username</th>
            <th>Email</th>
            <th>Role</th>
            <th>Date Created</th>
            <th>Password</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in userStore.getUsers" :key="item.userId">
            <td> {{ item.userId }} </td>
            <td> {{ item.username }} </td>
            <td> {{ item.email }} </td>
            <td> {{ item.role }} </td>
            <td> {{ item.dateCreated }} </td>
            <td> {{ item.password }} </td>
            <td>
              <button class="crudBtn" @click="userDetails(item)">Details</button>
              <button class="crudBtn" @click="userEdit(item)">Edit</button>
              <button class="crudBtn" @click="userDelete(item)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="container">
      <button @click="userCreate()">Create</button>
    </div>

</template>

<script lang="ts">
import { defineComponent} from "vue";
import { mapStores } from "pinia";
import { useUserStore } from "@/stores/";
import type { IUser } from "@/models/";
import ModelTitle from "@/components/ModelTitle.vue";

export default defineComponent({
  methods: {
    userDetails(user: IUser) {
      this.userStore.setCurrentUser(user);
      this.$router.push(`/userDetails/${user.userId}`);
    },
    userEdit(user: IUser) {
      this.userStore.setCurrentUser(user);
      this.$router.push(`/userEdit/${user.userId}`);
    },
    userDelete(user: IUser){
      this.userStore.setCurrentUser(user);
      this.$router.push(`/userDelete/${user.userId}`);
    },
    userCreate() {
      this.$router.push(`/userCreate`);
    }
  },
  computed:{
    ...mapStores(useUserStore)
  },
  components: {
    ModelTitle
  }
})
</script>