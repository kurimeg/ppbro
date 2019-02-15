<template>
  <v-layout row wrap>
    <v-flex xs12>
      <div class="profile-container">
        <img class="profile-image" :src="require('@/assets/profile-image.jpg')"/>
      </div>
    </v-flex>
    <v-flex xs12>
      <div class="profile-container">
        <v-subheader><h1>{{ account.name }}</h1></v-subheader>
      </div>
    </v-flex>
    <v-flex xs12>
      <div>自己PR</div>
    </v-flex>
  </v-layout>
  <!-- <v-divider light></v-divider> -->
</template>

<script>
export default {
  props: {
    account: Object
  }
}
</script>

<style scoped>
.profile-container{
  display: flex;
  justify-content: center;
}
.profile-image{
  height: 30vh;
  border-radius: 20%;
}
</style>
