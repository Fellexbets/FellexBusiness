<template>
<div>
<select class="dropdown col-2 " v-model="filter" :key="filter" @change="filterAnswers(filter)"> 
        <option v-bind:value= "null_value">Filter by True/False</option>
        <option v-for="filter in filters" :key="filter">{{ filter }}</option>

    </select>
    <label for="search">Search:
        <input class="searchbar " id="search" v-model="term" @keypress.enter="search(term)" />
    </label>
</div>
</template>


<script lang="ts">
import type { Theme } from "@/models";
const filters = [
    'All',
    'True',
    'False'
]

export default {
    props: [
        'filterAnswers',
        'search',
        'filter',
        'null_value'
    ],
    data() {
        return {
            term: '',
            filters,
        }
    }
}
</script>