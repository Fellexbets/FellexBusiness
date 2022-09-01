<template>
    <div class="searchDiv">
        <select class="dropdown col-2" v-model="sort" :key="sort" @change="sortQuizzes(sort)" id="sortQuizzes">
        <option v-bind:value= "null_value">Sort</option>
        <option v-for="sort in sorting" :key="sort">{{ sort }}</option>
    </select>

    <label for="search">Search:
        <input class="searchbar" id="searchQuizzes" v-model="term" @keypress.enter="searchQuizzes(term)" />
    </label>
</div>
</template>


<script lang="ts">
import type { Theme } from "@/models";
const sorting = [
    'Asc',
    'Desc'
]

export default {
    props: [
        'sortQuizzes',
        'searchQuizzes',
        'sort',
        'null_value'
    ],
    data() {
        return {
            term: '',
            sorting
        }
    }
}
</script>