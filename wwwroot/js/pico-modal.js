/*
 * Modal
 *
 * Pico.css - https://picocss.com
 * Copyright 2019-2024 - Licensed under MIT
 */

// Config
const isOpenClass = "modal-is-open"
const animationDuration = 0 // ms
let visibleModal = null

// Toggle modal
const toggleModal = (event) => {
  event.preventDefault()

  const modal = document.getElementById(event.currentTarget.dataset.target)
  const userId = event.currentTarget.dataset.id

  if (!modal) return

  // Populate the hidden input with the user ID
  const hiddenInput = modal.querySelector('input[name="Id"]')
  if (hiddenInput) {
    hiddenInput.value = userId
  }

  modal && (modal.open ? closeModal(modal) : openModal(modal))
}

// Open modal
const openModal = (modal) => {
  const { documentElement: html } = document

  html.classList.add(isOpenClass)

  modal.showModal()
}

// Close modal
const closeModal = (modal) => {
  visibleModal = null

  const { documentElement: html } = document

  html.classList.remove(isOpenClass)

  modal.close()
}

// Close with a click outside
document.addEventListener("click", (event) => {
  if (visibleModal === null) return

  const modalContent = visibleModal.querySelector("article")

  const isClickInside = modalContent.contains(event.target)

  !isClickInside && closeModal(visibleModal)
})

// Close with Esc key
document.addEventListener("keydown", (event) => {
  if (event.key === "Escape" && visibleModal)
    closeModal(visibleModal)
})

// Dinamically set the route-id for the delete link
const confirmDelete = (event) => {
  event.preventDefault()
  
  // Get the ID from the modal's data attribute
  const userId = document.getElementById('confirm-delete').dataset.id
  
  // Find the hidden delete link
  const deleteLink = document.getElementById('delete-confirm-link')
  
  // Set the route-id dynamically
  deleteLink.href = `/User/Delete/${userId}`
  
  // Programmatically click the hidden link
  deleteLink.click()
}
