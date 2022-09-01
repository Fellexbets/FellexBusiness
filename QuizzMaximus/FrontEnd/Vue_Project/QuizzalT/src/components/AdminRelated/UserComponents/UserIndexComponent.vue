<template>
    <div class="container">
      <ModelTitle title="User Index" />
      <Filters :filterRoles="filterRoles" :search="search" />
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
          <!--<tr v-for="item in userStore.getUsers" :key="item.userId">-->
          <tr v-for="item in users" :key="item.userId">
            <td> {{ item.userId }} </td>
            <td> {{ item.username }} </td>
            <td> {{ item.email }} </td>
            <td> {{ item.role }} </td>
            <td> {{ item.dateCreated }} </td>
            <td> {{ item.password }} </td>
            <td>
              <button class="mb-1 crudBtn d-flex flex-column" @click="userDetails(item)">Details</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="userEdit(item)">Edit</button>
              <button class="mb-1 crudBtn d-flex flex-column" @click="userDelete(item)">Delete</button>
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
import { useAuthStore } from "@/stores/AuthStore";
import Filters from "@/components/Filters/UsersFilters.vue";

export default defineComponent({
  data() {
    return {
      users: [] as IUser[]
    }
  },
  methods: {
    userDetails(user: IUser) {
      this.userStore.setCurrentUser(user);
      this.$emit('changePage', 'UserDetailsComponent');
    },
    userEdit(user: IUser) {
      this.userStore.setCurrentUser(user);
      this.$emit('changePage', 'UserEditComponent');
    },
    userDelete(user: IUser){
      this.userStore.setCurrentUser(user);
      this.$emit('changePage', 'UserDeleteComponent');
    },
    userCreate() {
      this.$emit('changePage', 'UserCreateComponent');
    }, 
    filterRoles(UserRole: any) {
      this.resetUsers()
      if (UserRole !== 'All') {
        this.users = this.userStore.getUsers.filter((user) => {
          return user.role == UserRole
          
        }
        )
      }
    },
    search(term: string) {
      this.resetUsers()
      this.users = this.users.filter((user) => {
        return user.username.toLowerCase().includes(term.toLowerCase())
      })
    },
    resetUsers() {
      this.users = this.userStore.getUsers;
    }
  },
  mounted() {
    this.users = this.userStore.getUsers
  },
  computed:{
    ...mapStores(useAuthStore,useUserStore)
  },
  components: {
    ModelTitle, Filters
  },
  emits: ["changePage"]
})
</script>